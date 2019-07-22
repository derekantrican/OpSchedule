using GanttChart;
using OpSchedule.Objects;
using OpSchedule.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static OpSchedule.Utilities.Enums;

namespace OpSchedule.Views
{
    public partial class ManagerEditor : Form
    {
        public ManagerEditor()
        {
            InitializeComponent();

            UpdatePersonListBox();

            UpdateAllComboBoxesWithPeople();

            comboBoxLocation.DataSource = Enum.GetValues(typeof(EmployeeLocation));
            comboBoxPermissions.DataSource = Enum.GetValues(typeof(Permissions));
            Common.Holidays.ForEach(h => checkedListBoxHolidays.Items.Add(h, h.Active));
            if (checkedListBoxHolidays.Items.Count > 0)
                checkedListBoxHolidays.SelectedIndex = 0;

            InitGanttChart();
        }

        private void InitGanttChart()
        {
            DateTime now = DateTime.UtcNow;
            int diff = (Settings.Instance.TimeZone.GetUtcOffset(now) - TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").GetUtcOffset(now)).Hours;

            DateTime start = Common.GetMondayForWeek(DateTime.Today);
            DateTime end = Common.GetFridayForWeek(DateTime.Today).AddDays(1);
            navGanttChart.GanttChart.StartDate = start;
            navGanttChart.GanttChart.EndDate = end;
            navGanttChart.GanttChart.StartHourInDay = 8 + diff;
            navGanttChart.GanttChart.EndHourInDay = 19 + diff;
            navGanttChart.GanttChart.DefaultTimeLabelFormat = "%ht";
            navGanttChart.GanttChart.DefaultDayLabelFormat = "dddd (M/d)";
            navGanttChart.GanttChart.MinTimeIntervalWidth = 30;
            NavGanttChart_UpdateHolidays();

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

        private void ButtonShowChartCounts_Click(object sender, EventArgs e)
        {
            ToggleChartCounts?.Invoke();
        }

        private void ButtonPublish_Click(object sender, EventArgs e)
        {
            if (!AreUsernamesValid(Common.AllData()))
                return;

            Serializers.SerializeSchedule(Common.AllData());
            Serializers.SerializeHolidays(Common.Holidays);

            //Merge pending edits into CurrentData
            Common.CurrentData = Common.AllData();
            Common.PendingPersonChanges.Clear();
            Common.PendingShiftChanges.Clear();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            if (!AreUsernamesValid(Common.AllData()))
                return;

            translator.Close();
            this.Close();
        }

        #region Events
        public delegate void RefreshGanttChartDelegate();
        public event RefreshGanttChartDelegate RefreshGanttChart;

        public delegate void ToggleChartCountsDelegate();
        public event ToggleChartCountsDelegate ToggleChartCounts;
        #endregion Events

        #region Shifts
        private void ButtonCopyPersonWeek_Click(object sender, EventArgs e)
        {
            if (!AreUsernamesValid(Common.AllData()))
                return;

            Person sourcePerson = comboBoxPerson1.SelectedItem as Person;
            DateTime sourceWeek = dateTimePickerSource1.Value;
            Person targetPerson = comboBoxPerson2.SelectedItem as Person;
            DateTime targetWeek = dateTimePickerTarget1.Value;
            int dayDiff = (Common.GetMondayForWeek(targetWeek) - Common.GetMondayForWeek(sourceWeek)).Days;

            if (sourcePerson == targetPerson &&
                Common.GetMondayForWeek(sourceWeek) == Common.GetMondayForWeek(targetWeek))
            {
                return;
            }

            List<Shift> shiftsToCopy = sourcePerson.GetShiftsForWeek(sourceWeek);
            shiftsToCopy.ForEach(p => //Adjust shiftsToCopy to targetWeek
            {
                p.StartTime.AddDays(dayDiff);
                p.EndTime.AddDays(dayDiff);
            });
            List<Shift> filteredShifts = FilterListByConflicts(targetPerson.GetShifts(), shiftsToCopy);

            //Update main form's gantt chart
            filteredShifts.ForEach(p => Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(p, targetPerson, Edit.Add)));
            RefreshGanttChart?.Invoke();
        }

        private void ButtonCopyPersonDate_Click(object sender, EventArgs e)
        {
            if (!AreUsernamesValid(Common.AllData()))
                return;

            Person sourcePerson = comboBoxPerson3.SelectedItem as Person;
            DateTime sourceDay = dateTimePickerSource2.Value;
            Person targetPerson = comboBoxPerson4.SelectedItem as Person;
            DateTime targetDay = dateTimePickerTarget2.Value;
            int dayDiff = (targetDay - sourceDay).Days;

            if (sourcePerson == targetPerson && sourceDay.Date == targetDay.Date)
                return;

            List<Shift> shiftsToCopy = sourcePerson.GetShiftsForDay(sourceDay);
            shiftsToCopy.ForEach(p => //Adjust shiftsToCopy to targetDay
            {
                p.StartTime.AddDays(dayDiff);
                p.EndTime.AddDays(dayDiff);
            });
            List<Shift> filteredShifts = FilterListByConflicts(targetPerson.GetShifts(), shiftsToCopy);

            //Update main form's gantt chart
            filteredShifts.ForEach(p => Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(p, targetPerson, Edit.Add)));
            RefreshGanttChart?.Invoke();
        }

        private void ButtonCopyEntireSchedule_Click(object sender, EventArgs e)
        {
            if (!AreUsernamesValid(Common.AllData()))
                return;

            DateTime sourceWeek = dateTimePickerSource3.Value;
            DateTime targetWeek = dateTimePickerTarget3.Value;
            int dayDiff = (Common.GetMondayForWeek(targetWeek) - Common.GetMondayForWeek(sourceWeek)).Days;

            if (Common.GetMondayForWeek(sourceWeek) == Common.GetMondayForWeek(targetWeek))
                return;

            List<Person> peopleWithShiftsToCopy = new List<Person>();
            foreach (Person person in Common.AllData())
            {
                List<Shift> shiftsToCopy = person.GetShiftsForWeek(sourceWeek);
                shiftsToCopy.ForEach(p => //Adjust shiftsToCopy to targetWeek
                {
                    p.StartTime.AddDays(dayDiff);
                    p.EndTime.AddDays(dayDiff);
                });

                Person clone = person.Clone(false);
                clone.TimeBlocks.AddRange(shiftsToCopy);
                peopleWithShiftsToCopy.Add(clone);
            }

            List<Person> filteredPeopleWithShifts = FilterListByConflicts(peopleWithShiftsToCopy);
            foreach (Person person in filteredPeopleWithShifts)
            {
                foreach (Shift shift in person.GetShifts())
                    Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(shift, person, Edit.Add));
            }

            RefreshGanttChart?.Invoke();
        }

        private void ButtonClearScheduleDate_Click(object sender, EventArgs e)
        {
            if (!AreUsernamesValid(Common.AllData()))
                return;

            DateTime sourceDay = dateTimePickerSource4.Value;
            List<Shift> shiftsToRemove;
            if (comboBoxPerson5.SelectedIndex == 0)
            {
                foreach (Person person in Common.AllData())
                {
                    foreach (Shift shift in person.GetShiftsForDay(sourceDay))
                        Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(shift, person, Edit.Delete));
                }
            }
            else
            {
                Person sourcePerson = comboBoxPerson5.SelectedItem as Person;
                shiftsToRemove = sourcePerson.GetShiftsForDay(sourceDay);

                shiftsToRemove.ForEach(p => Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(p, sourcePerson, Edit.Delete)));
            }

            RefreshGanttChart?.Invoke();
        }

        private void ButtonClearScheduleWeek_Click(object sender, EventArgs e)
        {
            if (!AreUsernamesValid(Common.AllData()))
                return;

            DateTime sourceWeek = dateTimePickerSource5.Value;
            List<Shift> shiftsToRemove;
            if (comboBoxPerson6.SelectedIndex == 0)
            {
                List<Person> allPeople = Common.AllData();
                foreach (Person person in allPeople)
                {
                    foreach (Shift shift in person.GetShiftsForWeek(sourceWeek))
                        Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(shift, person, Edit.Delete));
                }
            }
            else
            {
                Person sourcePerson = comboBoxPerson6.SelectedItem as Person;
                shiftsToRemove = sourcePerson.GetShiftsForWeek(sourceWeek);

                shiftsToRemove.ForEach(p => Common.PendingShiftChanges.Add(new Tuple<Shift, Person, Edit>(p, sourcePerson, Edit.Delete)));
            }

            RefreshGanttChart?.Invoke();
        }
        #endregion Shifts

        #region People
        private void ListBoxPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPeople.SelectedItem != null)
            {
                Person selectedPerson = listBoxPeople.SelectedItem as Person;
                textBoxName.Text = selectedPerson.Text;
                textBoxUsername.Text = selectedPerson.Username;
                comboBoxLocation.SelectedItem = selectedPerson.EmployeeLocation;
                comboBoxPermissions.SelectedItem = selectedPerson.Permissions;
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (IsPersonValid(true))
            {
                Person person = new Person(textBoxName.Text);
                person.Username = textBoxUsername.Text;
                person.EmployeeLocation = (EmployeeLocation)Enum.Parse(typeof(EmployeeLocation), comboBoxLocation.SelectedValue.ToString());
                person.Permissions = (Permissions)Enum.Parse(typeof(Permissions), comboBoxPermissions.SelectedValue.ToString());

                //Update pending changes
                Common.PendingPersonChanges.Add(new Tuple<Person, Edit>(person, Edit.Add));
                UpdatePersonListBox();
                UpdateAllComboBoxesWithPeople();

                RefreshGanttChart?.Invoke();
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (IsPersonValid(false))
            {
                int index = listBoxPeople.SelectedIndex;
                Person selectedPerson = listBoxPeople.SelectedItem as Person;
                selectedPerson.Text = textBoxName.Text;
                selectedPerson.Username = textBoxUsername.Text;
                selectedPerson.EmployeeLocation = (EmployeeLocation)Enum.Parse(typeof(EmployeeLocation), comboBoxLocation.SelectedValue.ToString());
                selectedPerson.Permissions = (Permissions)Enum.Parse(typeof(Permissions), comboBoxPermissions.SelectedValue.ToString());

                //Update pending changes
                Common.PendingPersonChanges.Add(new Tuple<Person, Edit>(selectedPerson, Edit.Overwrite));
                UpdatePersonListBox();
                UpdateAllComboBoxesWithPeople();
                RefreshGanttChart?.Invoke();
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = DialogResult.Yes;

            Person selectedPerson = listBoxPeople.SelectedItem as Person;
            if (selectedPerson == null)
                return;

            int numShifts = selectedPerson.GetShifts().Where(p => p.Clickable).Count();
            if (numShifts > 0)
                res = MessageBox.Show($"{selectedPerson.Text} has {numShifts} shifts. Are you sure you want to delete them?", "Delete Person", MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
            {
                //Update pending changes
                Common.PendingPersonChanges.Add(new Tuple<Person, Edit>(selectedPerson, Edit.Delete));
                UpdatePersonListBox();
                UpdateAllComboBoxesWithPeople();
                RefreshGanttChart?.Invoke();
            }
        }

        private bool IsPersonValid(bool checkForDuplicateUsername)
        {
            if (checkForDuplicateUsername &&
                Common.AllData().FirstOrDefault(p => p.Username == textBoxUsername.Text) != null)
            {
                MessageBox.Show("That username already exists in the list");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Please enter a name");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                MessageBox.Show("Please enter a username");
                return false;
            }

            return true;
        }

        private bool AreUsernamesValid(List<Person> peopleToValidate, bool showMessage = true)
        {
            bool result = peopleToValidate.Any(p => string.IsNullOrEmpty(p.Username));

            if (showMessage && !result)
                MessageBox.Show("Please go to the \"People\" tab and make sure a username is defined for everyone");

            return result;
        }
        #endregion People

        #region Holidays
        private void CheckedListBoxHolidays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxHolidays.SelectedItem != null)
            {
                Holiday selectedHoliday = checkedListBoxHolidays.SelectedItem as Holiday;
                textBoxHolidayName.Text = selectedHoliday.Name;

                if (selectedHoliday.OverrideDate.HasValue)
                {
                    dateTimePickerHolidayDate.Value = selectedHoliday.OverrideDate.Value;
                    dateTimePickerHolidayDate.Checked = true;
                }
                else
                {
                    dateTimePickerHolidayDate.Checked = false;
                    dateTimePickerHolidayDate.Value = dateTimePickerHolidayDate.MinDate;
                }
            }
        }

        private void ButtonDeleteHoliday_Click(object sender, EventArgs e)
        {
            Holiday selectedHoliday = checkedListBoxHolidays.SelectedItem as Holiday;
            if (selectedHoliday != null)
            {
                int index = checkedListBoxHolidays.SelectedIndex;
                checkedListBoxHolidays.Items.Remove(checkedListBoxHolidays.SelectedItem);

                if (checkedListBoxHolidays.Items.Count > 0)
                {
                    if (index > checkedListBoxHolidays.Items.Count - 1)
                        checkedListBoxHolidays.SelectedIndex = checkedListBoxHolidays.Items.Count - 1;
                    else
                        checkedListBoxHolidays.SelectedIndex = index;
                }

                SyncHolidaysToCharts();
            }
        }

        private void ButtonAddHoliday_Click(object sender, EventArgs e)
        {
            Holiday holiday = new Holiday() { Active = true, Name = textBoxHolidayName.Text };
            if (dateTimePickerHolidayDate.Checked)
                holiday.OverrideDate = dateTimePickerHolidayDate.Value;
            else
                holiday.OverrideDate = null;

            checkedListBoxHolidays.Items.Add(holiday, true);

            SyncHolidaysToCharts();
        }

        private void ButtonSaveHoliday_Click(object sender, EventArgs e)
        {
            int index = checkedListBoxHolidays.SelectedIndex;
            bool holidayActive = checkedListBoxHolidays.GetItemChecked(index);
            Holiday selectedHoliday = checkedListBoxHolidays.SelectedItem as Holiday;
            selectedHoliday.Name = textBoxName.Text;
            if (dateTimePickerHolidayDate.Checked)
                selectedHoliday.OverrideDate = dateTimePickerHolidayDate.Value;
            else
                selectedHoliday.OverrideDate = null;

            checkedListBoxHolidays.Items.RemoveAt(index);
            checkedListBoxHolidays.Items.Insert(index, selectedHoliday);
            checkedListBoxHolidays.SetItemChecked(index, holidayActive);
            checkedListBoxHolidays.SelectedIndex = index;

            SyncHolidaysToCharts();
        }

        private void SyncHolidaysToCharts()
        {
            Common.Holidays.Clear();
            foreach (Holiday item in checkedListBoxHolidays.Items.Cast<Holiday>())
            {
                item.Active = checkedListBoxHolidays.GetItemChecked(checkedListBoxHolidays.Items.IndexOf(item));
                Common.Holidays.Add(item);
            }

            //Update main gantt chart with new holidays
            RefreshGanttChart?.Invoke();

            //Update preview gantt chart with new holidays
            NavGanttChart_UpdateHolidays();
        }
        #endregion Holidays

        #region Excel
        private ExcelTranslator translator = new ExcelTranslator();
        private void ButtonOpenExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                labelExcelFile.Text = openFile.FileName;

                if (File.Exists(labelExcelFile.Text))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    translator.LoadFile(labelExcelFile.Text);
                    comboBoxSheet.Items.AddRange(translator.GetSheetNames().ToArray());
                    comboBoxSheet.SelectedIndex = 0;
                    Cursor.Current = Cursors.Arrow;
                }
            }
        }

        private void ComboBoxSheet_DropDown(object sender, EventArgs e)
        {
            int maxWidth = 0, temp = 0;
            foreach (var obj in comboBoxSheet.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), comboBoxSheet.Font).Width;
                if (temp > maxWidth)
                    maxWidth = temp;
            }

            comboBoxSheet.DropDownWidth = maxWidth;
        }

        private void ButtonReadExcel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (File.Exists(labelExcelFile.Text))
            {
                try
                {
                    List<Person> translatedData = translator.TranslateExcel(comboBoxSheet.SelectedItem.ToString());
                    int shiftCount = translatedData.SelectMany(p => p.TimeBlocks).Where(p => p.Clickable).Count();
                    navGanttChart.GanttChart.Rows = translatedData.Cast<Row>().ToList();
                    navGanttChart.GanttChart.UpdateView();

                    MessageBox.Show($"Imported {shiftCount} shifts from excel");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error reading the excel document:\n\n" + ex.Message);
                }
            }

            Cursor.Current = Cursors.Arrow;
        }

        private void ButtonSendPeopleToChart_Click(object sender, EventArgs e)
        {
            foreach (Person person in navGanttChart.GanttChart.Rows.Cast<Person>())
                Common.PendingPersonChanges.Add(new Tuple<Person, Edit>(person.Clone(false), Edit.Add));

            UpdatePersonListBox();
            UpdateAllComboBoxesWithPeople();

            RefreshGanttChart?.Invoke();
        }

        private void ButtonSendShiftsToChart_Click(object sender, EventArgs e)
        {
            //Make sure everyone has usernames first (otherwise there will be some issues when trying to merge pending changes with current data)
            List<Person> allGanttPeople = navGanttChart.GanttChart.Rows.Cast<Person>().ToList();
            if (AreUsernamesValid(allGanttPeople, false) || AreUsernamesValid(Common.AllData(), false))
            {
                ButtonSendPeopleToChart_Click(null, null);
                MessageBox.Show("Please go to the \"People\" tab and make sure a username is defined for everyone");

                return;
            }

            List<Person> filteredPeople = FilterListByConflicts(allGanttPeople);

            //Update pending changes
            filteredPeople.ForEach(p => Common.PendingPersonChanges.Add(new Tuple<Person, Edit>(p, Edit.Merge)));
            RefreshGanttChart?.Invoke();

            UpdatePersonListBox();
            UpdateAllComboBoxesWithPeople();
        }
        #endregion Excel

        #region Helper Methods
        private void UpdateAllComboBoxesWithPeople(bool updateSelection = true)
        {
            int comboBox1Index = comboBoxPerson1.SelectedIndex;
            int comboBox2Index = comboBoxPerson2.SelectedIndex;
            int comboBox3Index = comboBoxPerson3.SelectedIndex;
            int comboBox4Index = comboBoxPerson4.SelectedIndex;
            int comboBox5Index = comboBoxPerson5.SelectedIndex;
            int comboBox6Index = comboBoxPerson6.SelectedIndex;

            UpdatePersonComboBox(comboBoxPerson1);
            UpdatePersonComboBox(comboBoxPerson2);
            UpdatePersonComboBox(comboBoxPerson3);
            UpdatePersonComboBox(comboBoxPerson4);
            UpdatePersonComboBox(comboBoxPerson5, true);
            UpdatePersonComboBox(comboBoxPerson6, true);

            if (updateSelection)
            {
                comboBoxPerson1.SelectedIndex = comboBox1Index > -1 && comboBox1Index < comboBoxPerson1.Items.Count ? comboBox1Index : 0;
                comboBoxPerson2.SelectedIndex = comboBox2Index > -1 && comboBox2Index < comboBoxPerson2.Items.Count ? comboBox2Index : 0;
                comboBoxPerson3.SelectedIndex = comboBox3Index > -1 && comboBox3Index < comboBoxPerson3.Items.Count ? comboBox3Index : 0;
                comboBoxPerson4.SelectedIndex = comboBox4Index > -1 && comboBox4Index < comboBoxPerson4.Items.Count ? comboBox4Index : 0;
                comboBoxPerson5.SelectedIndex = comboBox5Index > -1 && comboBox5Index < comboBoxPerson5.Items.Count ? comboBox5Index : 0;
                comboBoxPerson6.SelectedIndex = comboBox6Index > -1 && comboBox6Index < comboBoxPerson6.Items.Count ? comboBox6Index : 0;
            }
        }

        private void UpdatePersonComboBox(ComboBox comboBox, bool includeAllOption = false)
        {
            comboBox.Items.Clear();
            comboBox.Items.AddRange(Common.AllData().ToArray());

            if (includeAllOption)
                comboBox.Items.Insert(0, new Person("ALL"));
        }

        private void UpdatePersonListBox()
        {
            int selectedIndex = listBoxPeople.SelectedIndex;
            listBoxPeople.Items.Clear();
            listBoxPeople.Items.AddRange(Common.AllData().ToArray());
            if (listBoxPeople.Items.Count > 0)
            {
                if (selectedIndex > -1 && selectedIndex < listBoxPeople.Items.Count - 1)
                    listBoxPeople.SelectedIndex = selectedIndex;
                else
                    listBoxPeople.SelectedIndex = 0;
            }
        }

        private List<Person> FilterListByConflicts(List<Person> newPeople)
        {
            //Todo: this should be a custom form in the future
            List<Person> filteredPeopleWithShifts = new List<Person>();

            Dictionary<Person, List<Shift>> conflictingShiftsByPerson = new Dictionary<Person, List<Shift>>();
            foreach (Person person in newPeople)
            {
                Person matchingCurrentPerson = Common.AllData().Find(p => p.Username == person.Username);
                if (matchingCurrentPerson != null)
                {
                    List<Shift> conflictingShifts = person.GetShifts().Where(p =>
                    {
                        return matchingCurrentPerson.GetShifts().Any(s => p.StartTime >= s.StartTime && p.StartTime < s.EndTime) ||
                               matchingCurrentPerson.GetShifts().Any(s => p.EndTime > s.StartTime && p.EndTime <= s.EndTime);
                    }).ToList();

                    conflictingShiftsByPerson.Add(person, conflictingShifts);

                    //Add the person and any non-conflicting shifts to the result list
                    List<Shift> nonConflictingShifts = person.GetShifts();
                    nonConflictingShifts.RemoveAll(p => conflictingShifts.Contains(p));
                    person.TimeBlocks.Clear();
                    person.TimeBlocks = nonConflictingShifts.Cast<TimeBlock>().ToList();
                    filteredPeopleWithShifts.Add(person);
                }
                else
                    filteredPeopleWithShifts.Add(person);
            }

            int totalNumConflicting = conflictingShiftsByPerson.SelectMany(p => p.Value).Count();
            if (totalNumConflicting > 0)
            {
                DialogResult res1 = MessageBox.Show($"There are {totalNumConflicting} total shifts that conflict with current shifts. Do you want to go through them?\n\n" +
                                                   "- \"No\" will skip conflicting shifts\n- \"Cancel\" will cancel the current action and skip all people",
                                                   "Conflicting shifts", MessageBoxButtons.YesNo);

                if (res1 == DialogResult.No)
                    return filteredPeopleWithShifts;
                else if (res1 == DialogResult.Cancel)
                    return new List<Person>();

                //Sort through conflicting shifts and add them to the result list
                foreach (Person person in conflictingShiftsByPerson.Keys)
                {
                    foreach (Shift shift in conflictingShiftsByPerson[person])
                    {
                        //Get the shift that the new shift conflicts with (there may be multiple, but we'll just get the first one
                        Shift conflictingShift = Common.AllData().FirstOrDefault(p => p.Username == person.Username).GetShifts().FirstOrDefault(s =>
                        {
                            return (shift.StartTime >= s.StartTime && shift.StartTime < s.EndTime) ||
                                   (shift.EndTime > s.StartTime && shift.EndTime <= s.EndTime);
                        });

                        DialogResult res2 = MessageBox.Show($"Do you want to overwrite {conflictingShift} with {shift}?",
                                                           "Resolve conflict for " + person,
                                                           MessageBoxButtons.YesNo);

                        if (res2 == DialogResult.Yes)
                        {
                            Person matchingPerson = filteredPeopleWithShifts.Find(p => p.Username == person.Username);
                            if (matchingPerson != null)
                                matchingPerson.TimeBlocks.Add(shift);
                            else
                            {
                                matchingPerson = new Person()
                                {
                                    Username = person.Username,
                                    Text = person.Text,
                                    EmployeeLocation = person.EmployeeLocation,
                                    Permissions = person.Permissions
                                };
                                matchingPerson.TimeBlocks.Add(shift);

                                filteredPeopleWithShifts.Add(matchingPerson);
                            }
                        }
                    }
                }
            }

            return filteredPeopleWithShifts;
        }

        private List<Shift> FilterListByConflicts(List<Shift> currentShifts, List<Shift> newShifts)
        {
            //Todo: this should be a custom form in the future
            List<Shift> conflictingShifts = newShifts.Where(p => 
            {
                return currentShifts.Any(s => p.StartTime >= s.StartTime && p.StartTime < s.EndTime) ||
                       currentShifts.Any(s => p.EndTime > s.StartTime && p.EndTime <= s.EndTime);
            }).ToList();

            if (conflictingShifts.Count > 0)
            {
                DialogResult res1 = MessageBox.Show($"There are {conflictingShifts.Count} shifts that conflict with current shifts. Do you want to go through them?\n\n" +
                                                   "- \"No\" will skip conflicting shifts\n- \"Cancel\" will cancel the current action and skip all shifts",
                                                   "Conflicting shifts", MessageBoxButtons.YesNo);

                if (res1 == DialogResult.No)
                {
                    newShifts.RemoveAll(p => conflictingShifts.Contains(p));
                    return newShifts;
                }
                else if (res1 == DialogResult.Cancel)
                    return new List<Shift>();

                foreach (Shift shift in conflictingShifts)
                {
                    //Get the shift that the new shift conflicts with (there may be multiple, but we'll just get the first one
                    Shift conflictingShift = currentShifts.FirstOrDefault(s =>
                    {
                        return (shift.StartTime >= s.StartTime && shift.StartTime < s.EndTime) || 
                               (shift.EndTime > s.StartTime && shift.EndTime <= s.EndTime);
                    });

                    DialogResult res2 = MessageBox.Show($"Do you want to overwrite {conflictingShift} with {shift}?",
                                                       "Resolve conflict",
                                                       MessageBoxButtons.YesNo);
                    if (res2 == DialogResult.No)
                        newShifts.Remove(shift);
                }
            }

            return newShifts;
        }
        #endregion Helper Methods
    }
}
