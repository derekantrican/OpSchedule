using GanttChart;
using OpSchedule.Objects;
using OpSchedule.Utilities;
using OpSchedule.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OpSchedule.Utilities.Enums;

namespace OpSchedule
{
    public partial class Main : Form
    {
        /*================================================================
         *              CURRENT FEATURES
         * - Double-click to add shift to person (for managers only)
         * - Single-click shift to see info (primitive info pane)
         * - Timeblocks have notes
         * - "Now Indicator" shows where the current time is
         * - Timezone changing
         * - Person adding/removing (via the Demo Editor for now)
         * - "Tier counter": how many people on each tier right now (via the Demo Editor for now)
         * - Setting to "Automatically adjust timezone" that will disable the manual timezone setting and automatically set your timezone based on computer timezone
         * - Ribbon (initial filters, settings)
         * - Custom filters (shift title contains {text}, shift notes contain {text}, shift is {color}, and "inverted" options)
         * - Settings window (selected timezone, feedback, about)
         * - Basic "Sync" features (reading from server on startup & writing/reading data via Demo Editor)
         * - Users & permissions (AEs should not be allowed to make changes to the schedule)
         * - Handling updates
         * - Should save the location & size that it had when it was closed, then restore that when opened (while also checking to make sure it is visible)
         * - "Sync" feature of manager makes changes, AE programs all check for changes
         * - Logging (similar to Bulk Edit where a Common.Log function also writes to the status strip)
         * - Ability to navigate to different weeks or to a certain day ("Go to...")
         * - Notifications of upcoming shifts & schedule changes
         * - A "Teams" button next to names on the headers that will start a message with that person
         * - Ribbon counts for tiers
         * - Tier counts displayed next to every hour (below or above the chart)
         * - Merge "User" properties into "Person". There shouldn't be a "Users.xml" file - just everything stored in the Schedule.xml file
         * - Holidays shown in chart
         * - Custom serialization of the schedule     
         * - Manager Editor
         *      - People editing
         *      - Import excel (To import the old schedule - WON'T SUPPORT NOTE IMPORTING)
         *      - Can customize which holidays are shown in chart (turn on/off or add custom ones)
         *      - Would allow for quick adding/editing/deleting of people & shifts. Quick enough to be comparable to editing the spreadsheet version (or better)
         *      - Adding people would be by username with a selected "display name"
         *      - Functions to make this quicker like "Copy schedule from {person} to this person", "Copy this person's schedule from {week}", etc
         *      - Over each hour, show counts for operator, tiers, etc (like the spreadsheet version)
         *      - Writing schedule & holidays to the server
         * 
         * 
         *              REQUIRED FEATURES BEFORE RELEASE
         * - An option for "list view" that would end up looking very similar to the old excel schedule. Basically, instead of having navigation buttons on one gantt chart,
         *      we would list multiple gantt charts (~52, enough to cover the whole year) in a list that all pull from the same data list but have different Start/End Dates
         * - Add buttons for special views (Day/Week). Maybe support a "month" view in the future?
         *
         * 
         * - Then update the Update.txt file on the server
         * 
         * 
         * 
         *              FUTURE FEATURES
         * - Maybe make the ribbon layout look better? (kinda like all the buttons squished to the left with the settings on the far right? Not sure what would look best)
         * - Code cleanup and organization
         * - Improve ribbon counts for tiers (better display)
         * - Improve infopane (support hyperlinks)
         * - Drag & drop shifts (would be nice to include in first release if easy)
         * - Drag to resize shifts (would be nice to include in first release if easy)
         * - Should be able to edit blockouts? (warning: a lot of things rely on "Clickable" but maybe they should work differently)
         * - Shift swapping (and everything that comes with it)
         * - User with read-only (eg AE) should be able to change the colors to their liking? (eg Pink for Tier 2)
         *      (MAYBE NOT - WOULD HAVE TO REQUIRE SOME EXTRA WORK WHEN COMPARING SERVER DATA TO CLIENT DATA)
         * - User with read-only can customize the timerange (eg "5am to 8pm")? (All users or just manager?)
         * - Recurrence on shifts? (eg "repeat this shift every week" or "repeat this shift every 2 weeks")
         * - "Templates" for creating shifts for a person (eg "Mon 8-11, 13-17; Wed 11-13...etc" along with tier shifts)
         *
         *
         *================================================================
         */

        public List<Filter> DefaultFilters;
        public System.Timers.Timer UpdateDataTimer = new System.Timers.Timer();
        public Main()
        {
            Common.CheckRequiredDirectoriesExist();
            Common.UpdateStatusStrip += UpdateStatusStrip;

            Settings.ReadSettings();
            InitDefaultFilters();

            InitializeComponent();

            Common.DeleteOldVersions();
            Task.Run(() => Common.CheckForUpdate());

            InitGanttChart();

            List<Person> serverData = Serializers.DeserializeSchedule(5);
            serverData.ForEach(p => p.Icon = Properties.Resources.Teams_icon);
            Common.CurrentData = serverData;
            navGanttChart.GanttChart.Rows = Common.GanttData();
            Common.CurrentlyUsedColors = Common.CurrentData.SelectMany(p => p.TimeBlocks).Where(p => p.Clickable).Select(p => p.Color).Distinct().ToList();

            LoadSettingsValues(); //Load timezone, etc

            UpdateRibbonForUser();

            InitUpdateDataTimer();
        }

        private void InitUpdateDataTimer()
        {
            UpdateDataTimer.Interval = 20 * 1000;
            UpdateDataTimer.Start();
            UpdateDataTimer.Elapsed += UpdateDataTimer_Tick;
            UpdateDataTimer.Enabled = true;
        }

        private void UpdateRibbonForUser()
        {
            tableLayoutPanelRibbon.Controls.Clear();
            tableLayoutPanelRibbon.ColumnStyles.Clear();

            List<Control> ribbonButtons = new List<Control>();

            if (CurrentUser?.Permissions == Permissions.Manager)
            {
                ribbonButtons = new List<Control>()
                {
                    ribbonButtonOpenEditor,
                    //ribbonButtonLoadDemoData, //Todo: remove before release
                    //ribbonButtonClearData, //Todo: remove before release
                    ribbonCounterOperator,
                    ribbonCounterTier1,
                    ribbonCounterTier2,
                    ribbonButtonFilter,
                    ribbonButtonSettings
                };
            }
            else
            {
                ribbonButtons = new List<Control>()
                {
                    //ribbonButtonOpenEditor, //Todo: remove before release
                    //ribbonButtonLoadDemoData, //Todo: remove before release
                    //ribbonButtonClearData, //Todo: remove before release
                    ribbonCounterOperator,
                    ribbonCounterTier1,
                    ribbonCounterTier2,
                    ribbonButtonFilter,
                    ribbonButtonSettings
                };
            }

            tableLayoutPanelRibbon.ColumnCount = ribbonButtons.Count;

            for (int i = 0; i < ribbonButtons.Count; i++)
                tableLayoutPanelRibbon.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / ribbonButtons.Count));

            foreach (Control c in ribbonButtons)
            {
                tableLayoutPanelRibbon.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / ribbonButtons.Count));
                tableLayoutPanelRibbon.Controls.Add(c, ribbonButtons.IndexOf(c), 0);
            }
        }

        private void UpdateDataTimer_Tick(object sender, EventArgs e) //Check for new data from the server (Settings.Instance.NotifyScheduleChange)
        {
            if (!Common.HasVPNConnection)
                return;

            if (CurrentUser?.Permissions == Permissions.Engineer ||
                (CurrentUser?.Permissions == Permissions.Manager && Common.PendingPersonChanges.Count == 0 && Common.PendingShiftChanges.Count == 0))
            {
                Common.Holidays = null; //Force hoidays to "re-fetch" from server

                List<Person> readData = Serializers.DeserializeSchedule(Settings.Instance.TimeZone);
                if (readData != null && readData.Count > 0)
                {
                    readData.ForEach(p => p.Icon = Properties.Resources.Teams_icon);

                    if (!Common.IsDataSame(Common.CurrentData, readData))
                    {
                        //Sync "AlertedUser" property to the new data
                        if (Common.CurrentData.Count > 0)
                        {
                            Common.CurrentData.FirstOrDefault(p => p.Username == CurrentUser?.Username)?.GetShifts().ForEach(shift =>
                            {
                                Shift matchingShift = readData.FirstOrDefault(p => p.Username == CurrentUser?.Username)?.GetShifts().Find(s => s.Equals(shift));
                                if (matchingShift != null)
                                    matchingShift.AlertedUser = shift.AlertedUser;
                            });
                        }

                        List<Person> newPersonsNotInCurrent = new List<Person>();
                        foreach (Person person in readData)
                        {
                            if (Common.CurrentData.Find(p => p.Equals(person)) == null)
                                newPersonsNotInCurrent.Add(person);
                        }

                        this.Invoke((MethodInvoker)delegate
                        {
                            Common.CurrentData = readData;
                            navGanttChart.GanttChart.Rows = Common.GanttData();
                            navGanttChart.GanttChart.UpdateView();
                        });

                        List<Shift> changedShifts = newPersonsNotInCurrent.FirstOrDefault(p => p.Username == CurrentUser?.Username)?.GetShifts()
                                                        .Where(p => p.Clickable).ToList();
                        if (Settings.Instance.NotifyScheduleChange && changedShifts != null && changedShifts.Count > 0)
                            ShowNotification("There has been a change to your schedule", string.Join("\n", changedShifts));

                        UpdateStatusStrip("Chart data updated", overwriteErrors: false);
                    }
                }
            }
        }

        private void InitGanttChart()
        {
            DateTime start = Common.GetMondayForWeek(DateTime.Today);
            DateTime end = Common.GetFridayForWeek(DateTime.Today).AddDays(1);
            navGanttChart.GanttChart.StartDate = start;
            navGanttChart.GanttChart.EndDate = end;
            navGanttChart.GanttChart.StartHourInDay = 8;
            navGanttChart.GanttChart.EndHourInDay = 19;
            navGanttChart.GanttChart.DefaultTimeLabelFormat = "%ht";
            navGanttChart.GanttChart.DefaultDayLabelFormat = "dddd (M/d)";
            navGanttChart.GanttChart.ShowNowIndicator = true;
            navGanttChart.GanttChart.MinTimeIntervalWidth = 30;
            foreach (Holiday holiday in Common.Holidays)
            {
                if (holiday.Active)
                    navGanttChart.GanttChart.Holidays.Add(holiday.HolidayDate, holiday.Name);
            }

            navGanttChart.GanttChart.RowIconSingleClick += GanttChart_RowIconSingleClick;
            navGanttChart.GanttChart.TimeBlockSingleClick += GanttChart_TimeBlockSingleClick;
            navGanttChart.GanttChart.TimeBlockDoubleClick += GanttChart_TimeBlockDoubleClick;
            navGanttChart.GanttChart.MainCanvasDoubleClick += GanttChart_MainCanvasDoubleClick;
            navGanttChart.GanttChart.NowTick += GanttChart_NowTick;
            navGanttChart.OnBeginNavigate += NavGanttChart_UpdateHolidays;
        }

        private void NavGanttChart_UpdateHolidays()
        {
            navGanttChart.GanttChart.Holidays.Clear();

            int refYear = navGanttChart.GanttChart.StartDate.Year;
            foreach (Holiday holiday in Common.Holidays)
            {
                if (holiday.Active)
                    navGanttChart.GanttChart.Holidays.Add(holiday.CalcHolidayDate(refYear), holiday.Name);
            }
        }

        private void GanttChart_NowTick() //Check to see if any shifts are upcoming (Settings.Instance.NotifyBeforeShift)
        {
            CheckForUpcomingShifts();
            UpdateRibbonCounts();
        }

        private void CheckForUpcomingShifts()
        {
            DateTime now = DateTime.Now;

            List<Shift> shiftsForCurrentUser = Common.CurrentData.FirstOrDefault(p => p.Username == CurrentUser?.Username)?.GetShifts();
            if (shiftsForCurrentUser != null)
            {
                shiftsForCurrentUser.RemoveAll(p => !p.Clickable); //Remove "blockouts"
                shiftsForCurrentUser.RemoveAll(p => p.StartTime >= now && p.StartTime < DateTime.Today.AddDays(1)); //Leave only shifts that are coming up today

                List<Shift> upcomingShifts = shiftsForCurrentUser.Where(p =>
                {
                    double diff = (p.StartTime - now).TotalMinutes;
                    if (diff < Settings.Instance.NotifyMinBeforeShift && diff > 0)
                        return true;
                    else
                        return false;
                }).ToList();
                upcomingShifts.RemoveAll(p => p.AlertedUser); //Remove where we have already alerted the user

                if (upcomingShifts.Count > 0 && Settings.Instance.NotifyBeforeShift)
                {
                    string title;
                    if (upcomingShifts.Count == 1)
                        title = "You have an upcoming shift";
                    else
                        title = "You have upcoming shifts";

                    foreach (Shift shift in shiftsForCurrentUser)
                    {
                        if (upcomingShifts.Find(p => p.Equals(shift)) != null)
                            shift.AlertedUser = true;
                    }

                    ShowNotification(title, string.Join("\n", upcomingShifts));
                }
            }
        }

        private void UpdateRibbonCounts()
        {
            DateTime now = DateTime.Now;

            int opCount, tier1Count, tier2Count;
            List<Shift> nowShifts = navGanttChart.GanttChart.GetNowTimeBlocks().SelectMany(p => p.Value).Cast<Shift>().ToList();
            opCount = nowShifts.Where(p => p.Text == "X").Count();
            tier1Count = nowShifts.Where(p => p.Text == "1").Count();
            tier2Count = nowShifts.Where(p => p.Text == "2").Count();

            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ribbonCounterOperator.Count = opCount;
                    ribbonCounterTier1.Count = tier1Count;
                    ribbonCounterTier2.Count = tier2Count;
                });
            }
        }

        private void ShowNotification(string title, string subtitle, bool autoHide = true, bool withSound = true)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (this.WindowState == FormWindowState.Minimized)
                    autoHide = false;

                Notification notify = new Notification(title, subtitle, autoHide, withSound);
                notify.ParentForm = this;
                notify.Show();

                Flash.FlashWindowEx(this);
            });
        }

        bool countsVisible = false;
        private void SetCountsVisiblity(bool visible)
        {
            if (visible)
            {
                navGanttChart.GanttChart.TopHeaderHeight = 60;

                List<Shift> allShifts = Common.CurrentData.SelectMany(p => p.TimeBlocks).Cast<Shift>().ToList();

                foreach (DateTime key in navGanttChart.GanttChart.TimeLabelFormats.Keys.ToList())
                {
                    int opCount, tier2Count;
                    opCount = allShifts.Where(p =>
                    {
                        if (key >= p.StartTime &&
                            key < p.EndTime &&
                            p.Text == "X")
                            return true;
                        else
                            return false;
                    }).Count();

                    tier2Count = allShifts.Where(p =>
                    {
                        if (key >= p.StartTime &&
                            key < p.EndTime &&
                            p.Text == "2")
                            return true;
                        else
                            return false;
                    }).Count();

                    navGanttChart.GanttChart.TimeLabelFormats[key] = $"%ht\n{opCount},{tier2Count}";
                }
            }
            else
            {
                navGanttChart.GanttChart.TopHeaderHeight = 40;

                foreach (DateTime key in navGanttChart.GanttChart.TimeLabelFormats.Keys.ToList())
                    navGanttChart.GanttChart.TimeLabelFormats[key] = "%ht";
            }
        }

        private void GanttChart_RowIconSingleClick(RowClickedEventArgs e)
        {
            Person person = e.ClickedRow as Person;
            StartSkypeMessage($"<sip:{person.SkypeIdentity}>");
        }

        private void InitDefaultFilters()
        {
            List<Filter> filters = new List<Filter>();

            Filter clearFilters = new Filter("---CLEAR FILTERS---");
            filters.Add(clearFilters);

            Filter onlyCinci = new Filter("Only Cincinnati");
            onlyCinci.OnlyCinci = true;
            filters.Add(onlyCinci);

            Filter onlySeattle = new Filter("Only Seattle");
            onlySeattle.OnlySeattle = true;
            filters.Add(onlySeattle);

            DefaultFilters = filters;
        }

        private void LoadSettingsValues()
        {
            //--------------Set TimeZone--------------
            if (Settings.Instance.AutoAdjustTZ)
            {
                TimeZoneInfo local = TimeZoneInfo.Local;
                if (Common.TZDict.Values.Contains(local))
                    Settings.Instance.TimeZone = local;
                else
                    Settings.Instance.TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            }

            ChangeTimezone(TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"), Settings.Instance.TimeZone);
            //----------------------------------------

            //---------------Set Filter---------------
            if (Settings.Instance.SelectedFilterIndex <= DefaultFilters.Concat(Settings.Instance.CustomFilters).Count() - 1)
                FilterClicked(DefaultFilters.Concat(Settings.Instance.CustomFilters).ToList()[Settings.Instance.SelectedFilterIndex]);
            else
                FilterClicked(DefaultFilters[0]); //Clear filters as default
            //----------------------------------------

            //------Set Program Location & Size-------
            FormWindowState windowState = (FormWindowState)(int)Settings.Instance.ProgramStartupState;
            if (windowState == FormWindowState.Maximized)
            {
                Point savedLocation = Settings.Instance.ProgramStartupLocation;
                if (!Screen.AllScreens.Any(p => p.Bounds.Contains(savedLocation))) //Make sure program will be visible
                    savedLocation = new Point(0, 0);

                this.Location = savedLocation;

                this.WindowState = FormWindowState.Maximized;
            }
            else if (windowState == FormWindowState.Minimized)
            {
                Point savedLocation = Settings.Instance.ProgramStartupLocation;
                if (!Screen.AllScreens.Any(p => p.Bounds.Contains(savedLocation))) //Make sure program will be visible
                    savedLocation = new Point(0, 0);

                this.Location = savedLocation;

                this.Size = Settings.Instance.ProgramStartupSize;

                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                Point savedLocation = Settings.Instance.ProgramStartupLocation;
                if (!Screen.AllScreens.Any(p => p.Bounds.Contains(savedLocation))) //Make sure program will be visible
                    savedLocation = new Point(0, 0);

                this.Location = savedLocation;

                this.Size = Settings.Instance.ProgramStartupSize;
            }
            //----------------------------------------
        }

        private void OffsetTimesBy(int offset)
        {
            navGanttChart.GanttChart.StartHourInDay += offset;
            navGanttChart.GanttChart.EndHourInDay += offset;

            foreach (Person person in Common.CurrentData)
                person.OffsetShifts(offset);

            navGanttChart.GanttChart.UpdateView();
        }

        private void StartSkypeMessage(string arguments)
        {
            Common.Log("Composing a Teams message to " + arguments);

            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = "/C start im:\"" + arguments + "\"";
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.FileName = "cmd.exe";
            Process infoProcess = Process.Start(Info);
        }

        private Person CurrentUser
        {
            get
            {
                if (Debugger.IsAttached)
                    return new Person("") { Permissions = Permissions.Manager };
                else
                    return Common.CurrentData.FirstOrDefault(p => p.Username == Environment.UserName);
            }
        }

        private void GanttChart_MainCanvasDoubleClick(CanvasClickedEventArgs e)
        {
            if (e.ClickedLocation.HasValue && CurrentUser?.Permissions == Permissions.Manager)
            {
                ShiftEditor editor = new ShiftEditor(e.RelatedRow, e.ClickedLocation.Value);
                editor.ShiftResult += (result) => 
                {
                    if (result != null)
                    {
                        Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(result, e.RelatedRow as Person, Edit.Add));
                        Editor_RefreshGanttChart();
                    }
                };
                editor.ShowDialog();

                Common.CurrentlyUsedColors = Common.CurrentData.SelectMany(p => p.TimeBlocks).Where(p => p.Clickable).Select(p => p.Color).Distinct().ToList();;
            }
        }

        private void GanttChart_TimeBlockDoubleClick(TimeBlockClickedEventArgs e)
        {
            if (CurrentUser?.Permissions == Permissions.Manager)
            {
                ShiftEditor editor = new ShiftEditor(e.RelatedRow, e.ClickedTimeBlock as Shift);
                editor.ShiftResult += (result) =>
                {
                    if (result == null)
                        Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(e.ClickedTimeBlock as Shift, e.RelatedRow as Person, Edit.Delete)); //Delete timeblock from chart
                    else
                    {
                        //Update timeblock in chart
                        Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(e.ClickedTimeBlock as Shift, e.RelatedRow as Person, Edit.Delete));
                        Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(result as Shift, e.RelatedRow as Person, Edit.Add));
                    }

                    Editor_RefreshGanttChart();
                };
                editor.ShowDialog();

                Common.CurrentlyUsedColors = Common.CurrentData.SelectMany(p => p.TimeBlocks).Where(p => p.Clickable).Select(p => p.Color).Distinct().ToList();
            }
        }

        private void GanttChart_TimeBlockSingleClick(TimeBlockClickedEventArgs e)
        {
            if (InfoPane.OpenInfoPane != null)
                InfoPane.OpenInfoPane.Close();

            InfoPane infoPane = new InfoPane(e.ClickedTimeBlock as Shift, e.RelatedRow as Person);
            infoPane.Show();
            Point p = new Point(e.CursorLocation.X + this.Left + navGanttChart.Left + 8 /*Todo: not sure why the reason for the "8"*/, 
                                e.CursorLocation.Y + this.Top + navGanttChart.Top + 31 /*Todo: not sure why the reason for the "31"*/);
            infoPane.Location = p;
        }

        private void RibbonButtonOpenEditor_MouseClick(object sender, MouseEventArgs e)
        {
            ManagerEditor editor = new ManagerEditor();
            editor.RefreshGanttChart += Editor_RefreshGanttChart;
            editor.ToggleChartCounts += () => 
            {
                SetCountsVisiblity(countsVisible);
                countsVisible = !countsVisible;
            };
            editor.Show();
        }

        private void Editor_RefreshGanttChart()
        {
            NavGanttChart_UpdateHolidays();

            List<Row> rows = Common.GanttData();
            foreach (Row row in rows)
            {
                if (!string.IsNullOrEmpty((row as Person).Username))
                    row.Icon = Properties.Resources.Teams_icon;
            }

            navGanttChart.GanttChart.Rows = rows;
            navGanttChart.GanttChart.UpdateView();

            Common.CurrentlyUsedColors = navGanttChart.GanttChart.Rows.SelectMany(p => p.TimeBlocks).Where(p => p.Clickable).Select(p => p.Color).Distinct().ToList();
        }

        private void RibbonButtonFilter_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.ShowImageMargin = false;

            //-----------ADD ITEMS-------------
            foreach (Filter filter in DefaultFilters)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(filter.Name);
                menuItem.Click += (s, args) => { FilterClicked(filter); };
                menuItem.Tag = filter;
                contextMenu.Items.Add(menuItem);
            }
            contextMenu.Items.Insert(1, new ToolStripSeparator()); //Insert after the "clear filters" option

            if (Settings.Instance.CustomFilters.Count > 0)
            {
                contextMenu.Items.Add(new ToolStripSeparator());
                foreach (Filter filter in Settings.Instance.CustomFilters)
                {
                    ToolStripMenuItem menuItem = new ToolStripMenuItem(filter.Name);
                    menuItem.Click += (s, args) => { FilterClicked(filter); };
                    menuItem.Tag = filter;
                    contextMenu.Items.Add(menuItem);
                }
            }

            contextMenu.ItemClicked += (s, args) => 
            {
                ToolStripMenuItem clickedFilter = args.ClickedItem as ToolStripMenuItem;
                if (clickedFilter.Tag != null && DefaultFilters.Concat(Settings.Instance.CustomFilters).Contains(clickedFilter.Tag as Filter))
                    Settings.Instance.SelectedFilterIndex = DefaultFilters.Concat(Settings.Instance.CustomFilters).ToList().IndexOf(clickedFilter.Tag as Filter);
            };

            contextMenu.Show((sender as RibbonButton), e.Location);
        }

        private void FilterClicked(Filter filter)
        {
            List<Person> filteredData = Common.CurrentData;

            //Make all visible (clear any existing filters)
            foreach (Person person in filteredData)
            {
                person.IsVisible = true;

                foreach (TimeBlock timeBlock in person.TimeBlocks)
                    timeBlock.IsVisible = true;
            }

            if (filter.OnlyCinci)
                filteredData.ForEach(p => p.IsVisible = (p as Person).EmployeeLocation == EmployeeLocation.Cincinnati);
            else if (filter.OnlySeattle)
                filteredData.ForEach(p => p.IsVisible = (p as Person).EmployeeLocation == EmployeeLocation.Seattle);
            else
            {
                if (filter.InvertFilter)
                {
                    if (!string.IsNullOrEmpty(filter.TextContains))
                    {
                        filteredData.ForEach(p => (p as Person).TimeBlocks.ForEach(s =>
                        {
                            if (s.Clickable && s.IsVisible)
                                s.IsVisible = !(s as Shift).Text.Contains(filter.TextContains);
                        }));
                    }

                    if (!string.IsNullOrEmpty(filter.NoteContains))
                    {
                        filteredData.ForEach(p => (p as Person).TimeBlocks.ForEach(s =>
                        {
                            if (s.Clickable && s.IsVisible)
                                s.IsVisible = !(s as Shift).Notes.Contains(filter.TextContains);
                        }));
                    }

                    if (filter.Color.HasValue)
                    {
                        filteredData.ForEach(p => (p as Person).TimeBlocks.ForEach(s => 
                        {
                            if (s.Clickable && s.IsVisible)
                                s.IsVisible = (s as Shift).Color != filter.Color.Value;
                        }));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(filter.TextContains))
                    {
                        filteredData.ForEach(p => (p as Person).TimeBlocks.ForEach(s =>
                        {
                            if (s.Clickable && s.IsVisible)
                                s.IsVisible = (s as Shift).Text.Contains(filter.TextContains);
                        }));
                    }

                    if (!string.IsNullOrEmpty(filter.NoteContains))
                    {
                        filteredData.ForEach(p => (p as Person).TimeBlocks.ForEach(s =>
                        {
                            if (s.Clickable && s.IsVisible)
                                s.IsVisible = (s as Shift).Notes.Contains(filter.TextContains);
                        }));
                    }

                    if (filter.Color.HasValue)
                    {
                        filteredData.ForEach(p => (p as Person).TimeBlocks.ForEach(s =>
                        {
                            if (s.Clickable && s.IsVisible)
                                s.IsVisible = (s as Shift).Color == filter.Color.Value;
                        }));
                    }
                }
            }

            navGanttChart.GanttChart.Rows = Common.GanttData();
            navGanttChart.GanttChart.UpdateView();
        }

        private void RibbonButtonSettings_MouseClick(object sender, MouseEventArgs e)
        {
            TimeZoneInfo oldTimeZone = Settings.Instance.TimeZone;

            SettingsWindow settings = new SettingsWindow();
            settings.ShowDialog();

            if (Settings.Instance.AutoAdjustTZ)
            {
                TimeZoneInfo local = TimeZoneInfo.Local;
                if (Common.TZDict.Values.Contains(local))
                    Settings.Instance.TimeZone = local;
                else
                    Settings.Instance.TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            }

            if (Settings.Instance.TimeZone != oldTimeZone)
                ChangeTimezone(oldTimeZone, Settings.Instance.TimeZone);
        }

        private void ChangeTimezone(TimeZoneInfo oldTimezone, TimeZoneInfo newTimezone)
        {
            DateTime now = DateTime.UtcNow;
            int diff = (newTimezone.GetUtcOffset(now) - oldTimezone.GetUtcOffset(now)).Hours;
            OffsetTimesBy(diff);

            navGanttChart.GanttChart.NowIndicatorHourOffset = (newTimezone.GetUtcOffset(now) - TimeZoneInfo.Local.GetUtcOffset(now)).Hours;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Common.PendingPersonChanges.Count > 0 || Common.PendingShiftChanges.Count > 0)
            {
                int pendingChanges = Common.PendingPersonChanges.Count + Common.PendingShiftChanges.Count;
                DialogResult res = MessageBox.Show($"You have {pendingChanges} pending changes. Do you want to publish them?",
                                                   "Pending changes", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    if (Common.AllData().Any(p => string.IsNullOrEmpty(p.Username)))
                    {
                        MessageBox.Show("Some people have not been assigned usernames. Please go to the Manager Editor to fix them");
                        e.Cancel = true;
                        return;
                    }

                    Serializers.SerializeSchedule(Common.AllData());
                    Serializers.SerializeHolidays(Common.Holidays);

                    //Merge pending edits into CurrentData
                    Common.CurrentData = Common.AllData();
                    Common.PendingPersonChanges.Clear();
                    Common.PendingShiftChanges.Clear();
                }
            }

            Settings.Instance.ProgramStartupLocation = this.Location;
            Settings.Instance.ProgramStartupSize = this.Size;
            Settings.Instance.ProgramStartupState = (System.Windows.WindowState)(int)this.WindowState;

            Settings.SaveSettings();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!Common.HasVPNConnection)
                Common.LogError("NOT CONNECTED TO THE VPN. PLEASE CONNECT AND RESTART THE APPLICATION.", writeToStatusStrip: true);
        }

        private void UpdateStatusStrip(string text, bool error = false, bool overwriteErrors = true)
        {
            if (IsHandleCreated)
            {
                statusStrip?.Invoke((MethodInvoker)delegate
                {
                    if (!overwriteErrors &&
                        toolStripStatusLabel.ForeColor == Color.Red &&
                        !string.IsNullOrEmpty(toolStripStatusLabel.Text))
                        return;

                    if (error)
                        toolStripStatusLabel.ForeColor = Color.Red;
                    else
                        toolStripStatusLabel.ForeColor = Color.Black;

                    toolStripStatusLabel.Text = text;
                    statusStrip.Refresh();
                });
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized &&
                Notification.OpenNotifications.Count > 0)
                Notification.OpenNotifications.ForEach(p => p.StartAutoHideTimer());
        }

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);
        private const uint SW_RESTORE = 0x09;

        public void Restore()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ShowWindow(this.Handle, SW_RESTORE);
            }
        }
    }
}
