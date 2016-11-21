using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace OnoFile
{
    class FileTransfer
    {
        string FileName = null;
        string SeriesName = null;
        string EpisodeNo = null;
        string FileType = null;
        bool specialfile;
        bool validFileName;
        bool numberAdjust;

        string LastSeriesName = null;
        string SeriesLocation = null;

        int FolderCount = 0;

        private MainWindow _form = null; //reference to the form object or an actual object itself copied over? ~ Important knowledge to know for performance

        public FileTransfer(MainWindow form)
        {
            _form = form;
        }

        public void Logic(string downloadLocation, string endLocation, bool rename, bool special, bool folder, bool mkv, bool mp4, bool movie) //may change, a lot of parameters being sent here...
        {


            /*ALL OBJECTS USED IN MAIN FORM:
             * CHECKBOXES: renameCB, specialCB, folderCB, mkvCB, mp4CB
             * TEXTBOX: DLbox, videoBox - locations
             * LABEL: status - updates the user with information regarding whats happening, called using _form.UpdateStatus
             * 
             * --Important Info for this class: Changing winform object (button, textbox etc) requires 'invoke' to the thread controlling those objects or I/O error
             * 
             * 
            /*/

            //Main Method, logic for everything goes here with exception of dragging UI & folder browser
            //Files are processed one at a time - if there is a problem with one, then it wont break the others this way

            //run the test method and end if this is the case
            if (downloadLocation == "TEST" || endLocation == "TEST")
            {
                tests(downloadLocation, endLocation);
                _form.UpdateStatus("Created Test Files");
                return;
            }

            //its ok for DLloc + VIDloc to be the same, it means the folder has vid files in it but also series names to put those vids in
            try
            {
                _form.pBarVis(true);

                List<string> dlFiles = new List<string>(Directory.GetFiles(downloadLocation)); //List of files in the downloaded directory


                #region Remove Non-Videos
                for (int i = 0; i < dlFiles.Count; i++) //Remove any invalid files from the list
                {
                    if (dlFiles[i].EndsWith(".mkv"))
                    {
                        if (mkv != true)
                        {
                            dlFiles.RemoveAt(i);
                            i--;
                        }
                    }
                    else if (dlFiles[i].EndsWith(".mp4"))
                    {
                        if (mp4 != true)
                        {
                            dlFiles.RemoveAt(i);
                            i--;
                        }
                    }
                    else
                    {
                        dlFiles.RemoveAt(i);
                        i--;
                    }
                }
                #endregion

                int TOTAL_FILES = dlFiles.Count; //This is used for the progress bar to find out how many files we started with

                if (dlFiles.Count == 0)
                {
                    _form.UpdateStatus("Location contains no files");
                    _form.pBarVis(false);
                    return; //location contains no files.. just leave
                }

                //conversion logic begins
                for (; ; )
                {
                    _form.UpdateStatus("Files to process: " + dlFiles.Count);
                    //C:\--------\---------\---\-\ remover
                    string[] ArrayFile = dlFiles[0].Split('\\');
                    FileName = ArrayFile[ArrayFile.Length - 1];

                    if (dlFiles.Count == 0)
                    {
                        _form.UpdateStatus("All Files completed");
                        break; //location contains no files.. just leave
                    }

                    //RENAME
                    if (rename == true) FileName = FormatName(FileName, special);
                    else //Still need series name for folder - so get it below
                    {
                        SeriesName = FileName.Split('-')[0].Trim(); //name of series is before the '-'
                    }


                    //MOVE
                    List<string> SeriesFolder = new List<string>(Directory.GetDirectories(endLocation)); //Grab a list of all folders in the move to location
                    for (int i = 0; i < SeriesFolder.Count; i++)
                    {
                        //C:\--------\---------\---\-\ remover
                        ArrayFile = SeriesFolder[i].Split('\\');
                        SeriesFolder[i] = ArrayFile[ArrayFile.Length - 1];
                    }


                    //FOLDERS WITH YEAR AT THE END HAVE YEAR REMOVED 'Hunter x Hunter (2007)' should become 'Hunter x Hunter' unless files provided explicitly named with year
                    if (LastSeriesName != SeriesName) //Make sure the dialog box doesnt appear everytime if moving the same series in bulk, just assume its always the users choice
                    {
                        FolderCount = 0; //Since its a new series, revert back to default state

                        //SEARCH FOLDERS FOR SHOW NAME (AND CHECK IF MORE THAN ONE EXISTS) - RIP PERFORMANCE!
                        //Need to create a new list to store the old ones in, as it requires ragex on the brackets at the end (we dont want to change the original list)
                        List<int> Marker = new List<int>(SmartFolderSearch(SeriesFolder));

                        //We have discovered more than one folder with this name, alert the user and ask them to select which one
                        if (FolderCount > 1) //Currently only supports TWO folders with the same name - Could be improved in the future (requires physical form implementation of new pop-up with multiple choices)
                        {
                            DialogResult dialogResult = MessageBox.Show("Two folders named: " + SeriesName + ". \n\nDo you want to put it in the: \"" + SeriesFolder[Marker[Marker.Count - 1]] + "\" folder?", "", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes) //Put it into user selected folder
                            {
                                SeriesLocation = endLocation + "\\" + SeriesFolder[Marker[Marker.Count - 1]] + "\\";
                            }
                            else //put it in the other folder
                            {
                                SeriesLocation = endLocation + "\\" + SeriesFolder[Marker[0]] + "\\";
                            }
                            _form.UpdateStatus("User asked to select which folder the file belongs in (" + SeriesFolder[Marker[0]] + ") or (" + SeriesFolder[Marker[Marker.Count - 1]] + ")");
                        }
                        else if (FolderCount == 1) SeriesLocation = endLocation + "\\" + SeriesFolder[Marker[0]] + "\\"; //We've found a folder with the marker
                        else SeriesLocation = endLocation + "\\" + SeriesName + "\\"; //No folder found :( pass it a series name
                    }

                    if (folder == true || FolderCount > 0) //first cond = allowed to make new folder //second cond = the file exists! we have a file count greater then 0
                    {
                        string TimeStamp = DateTime.Now.ToString();
                        TimeStamp = TimeStamp.Replace("/", "-");
                        TimeStamp = TimeStamp.Replace(":", "^");

                        if (!Directory.Exists(SeriesLocation))
                        {
                            _form.UpdateStatus("Location did not exist for: " + SeriesName + " creating folder.");
                        }

                        Directory.CreateDirectory(SeriesLocation); //Doublechecking, if the folder isnt there make it (could be above but meh - precaution)
                        Directory.CreateDirectory(SeriesLocation + "Season 1");

                        int Season = 1; //default season is 1

                        if (Directory.GetDirectories(SeriesLocation).Length > 1) //If theres multiple seasons, we need to check which one is the latest
                        {                                                        //could be count length -1 if theres a special, but what if a random folder exists? not a good idea
                            string[] seasons = Directory.GetDirectories(SeriesLocation);
                            int i = seasons.Length;

                            string ComparatorConvert = null;
                            int Comparator = 0;


                            for (int x = 0; x < i; x++)
                            {
                                ComparatorConvert = seasons[x].Remove(0, seasons[x].Length - 2); //Get the last two parts of the season string (e.g. c:/---/---/season 10/ = 10)
                                ComparatorConvert.Trim();

                                try
                                {
                                    Comparator = Int32.Parse(ComparatorConvert);
                                    if (Comparator > Season) Season = Comparator; //If the next season has a higher number then the last, replace season
                                }
                                catch { }
                            }
                        }
                            if (rename == true && validFileName == true) //CONSIER: RENAME = TRUE BUT FILE WAS IN AN INVALID FORMAT
                            {
                                if (specialfile != true) FileName = SeriesName + " - " + "S0" + Season + "E" + EpisodeNo + FileType; //Since there are no season folders, it's season 1
                                else FileName = SeriesName + " - " + "S00E" + EpisodeNo + FileType; //RENAMING TO SEASON 0
                            }
                            try //Just as a reminder, dlFiles[0] is the current file being managed
                            {
                                var downloadFile = new DirectoryInfo(downloadLocation);
                                var DestinationFile = new DirectoryInfo(SeriesLocation + "Season " + Season + "\\"); //used in the else if
                                if (numberAdjust == true)//We have the season folder, and the number is weird - the user has indicated that the number is wrong for the season, let's fix that!
                                {
                                    List<string> SeasonFiles = new List<string>(Directory.GetFiles(SeriesLocation + "Season " + Season + "\\"));
                                    for (int i = 0; i < SeasonFiles.Count; i++) //Remove any invalid files from the list
                                    {
                                        if (SeasonFiles[i].EndsWith(".mkv"))
                                        {
                                            if (mkv != true)
                                            {
                                                SeasonFiles.RemoveAt(i);
                                                i--;
                                            }
                                        }
                                        else if (SeasonFiles[i].EndsWith(".mp4"))
                                        {
                                            if (mp4 != true)
                                            {
                                                SeasonFiles.RemoveAt(i);
                                                i--;
                                            }
                                        }
                                        else
                                        {
                                            SeasonFiles.RemoveAt(i);
                                            i--;
                                        }
                                    }

                                    //We have the amount of videos in the location.. Let's make the new video episode the latest one, so ++ 
                                    EpisodeNo = (SeasonFiles.Count + 1).ToString();
                                    if (specialfile != true) FileName = SeriesName + " - " + "S0" + Season + "E" + EpisodeNo + FileType; //Since there are no season folders, it's season 1
                                    else FileName = SeriesName + " - " + "S00E" + EpisodeNo + FileType; //RENAMING TO SEASON 0
                                }

                                if ((specialfile == true) && downloadFile.Exists)
                                {
                                    Directory.CreateDirectory(SeriesLocation + "Specials");

                                    _form.CLOSE_BUTTON_ENABLED(false); //important to stop corruption, could improve to stop process ending (maybe complex)
                                    if (!File.Exists(SeriesLocation + "Specials" + "\\" + FileName))
                                    {
                                        File.Move(dlFiles[0], SeriesLocation + "Specials" + "\\" + FileName); //Throw it in the specials folder
                                        _form.UpdateStatus("Moved " + FileName + " to " + SeriesLocation + "Specials" + "\\");
                                    }
                                    else//The file already exists, but since its a special we dont want to do a new season folder call
                                    {
                                        if (!File.Exists(SeriesLocation + "Specials" + "\\" + FileName)) //Someones trying to break it on purpose if it hits this and fails.. So bugger them
                                        {
                                            _form.UpdateStatus("The file: " + FileName + " already exists in " + SeriesLocation + "Specials" + " -- Moving with timestamp on files name");
                                            if (validFileName == true) FileName = SeriesName + " - " + "S00E" + EpisodeNo + " (" + TimeStamp + ")" + FileType;
                                            else FileName = FileName + " (" + TimeStamp + ")" + FileType;
                                            File.Move(dlFiles[0], SeriesLocation + "Specials" + "\\" + FileName); //Throw it in the specials folder
                                            _form.UpdateStatus("Moved " + FileName + "|| to " + SeriesLocation + "Specials" + "\\" + FileName );
                                        }
                                    }
                                    _form.CLOSE_BUTTON_ENABLED(true);

                                }
                                else if (DestinationFile.Exists && downloadFile.Exists) //CHECK IT EXISTS, ELSE RETURNS EXCEPTION AND FILE STREAM DOESNT CLOSE ~ smart programming c#
                                {
                                    _form.CLOSE_BUTTON_ENABLED(false);
                                    //Check if the file exists and inform the user if it should make a new season folder
                                    if (!File.Exists(SeriesLocation + "Season " + Season + "\\" + FileName)) //Someones trying to break it on purpose if it hits this and fails.. So bugger them
                                    {
                                        File.Move(dlFiles[0], SeriesLocation + "Season " + Season + "\\" + FileName);
                                        _form.UpdateStatus("Moved " + FileName + " || to " + SeriesLocation + "Season " + Season + "\\" + FileName);
                                    }
                                    else
                                    {
                                        DialogResult dialogResult = MessageBox.Show("There already exists: \n" + FileName + "\n In: \n" + SeriesLocation + "Season " + Season +"\n Do you want to create a new season for the file?", "", MessageBoxButtons.YesNo);
                                        if (dialogResult == DialogResult.Yes) //Time for shifting folders
                                        {
                                            //create next season logic
                                            Season++; //The season is increased by one
                                            FileName = SeriesName + " - " + "S0" + Season + "E" + EpisodeNo + FileType; //Update the file name to the new season
                                            Directory.CreateDirectory(SeriesLocation + "Season " + Season);
                                            File.Move(dlFiles[0], SeriesLocation + "Season " + Season + "\\" + FileName); //Interesting thing to consider: we always assume
                                            _form.UpdateStatus("Moved " + FileName + " || to " + SeriesLocation + "Season " + Season + "\\" + FileName); //the season given to the file is the latest, so this cannot exist
                                        }
                                        else //throw a timestamp on it and put in original location
                                        {
                                            _form.UpdateStatus("The file: " + FileName + " already exists in " + SeriesLocation + "Specials" + " -- Moving with timestamp on files name");
                                            if (validFileName) FileName = SeriesName + " - " + "S0" + Season + "E" +  EpisodeNo + " (" + TimeStamp + ")" + FileType;
                                            else FileName = FileName + " (" + TimeStamp + ")" + FileType;
                                            File.Move(dlFiles[0], SeriesLocation + "Season " + Season + "\\" + FileName);
                                            _form.UpdateStatus("Moved " + FileName + " || to " + SeriesLocation + "Season " + Season + "\\" + FileName);
                                        }
                                    }
                                    _form.CLOSE_BUTTON_ENABLED(true);
                                }
                                else _form.UpdateStatus("File: " + FileName + "' cannot be moved to its destination");
                                if (dlFiles.Count != 0) _form.UpdateProgressBar(dlFiles.Count, TOTAL_FILES);//CALC PERCENTAGE - DONT DIVIDE BY 0
                            }
                            catch
                            {
                                _form.UpdateStatus("Problem moving: " + SeriesName + " do you have the file open?");
                            }
                    }
                    else //NOT ALLOWED TO MAKE A NEW FOLDER
                    {   //and no folder exists so, just skip..
                        _form.UpdateStatus(FileName + "does not have a folder to be placed in, skipping");
                    }

                    try
                    {
                        _form.UpdateStatus("------------\r\n"); //End of procesing this file, making space for the next
                        dlFiles.RemoveAt(0);
                        LastSeriesName = SeriesName;
                        if (dlFiles.Count == 0) break;
                    }
                    catch { throw new Exception(); }


                }
            }
            catch (Exception ex)
            {
                // Get stack trace for the exception with source file information
                var st = new System.Diagnostics.StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                _form.UpdateStatus("Something went wrong. Inform developer that line: " + line + " of the code is poo.");
                return;
            }
            _form.UpdateStatus("Complete");
            _form.ShowLogBtn(true);

        }
        string FormatName(string fileString, bool special)
        {
            //CHECK CONTENTS FOR 'OVA' // 'SPECIAL' or 'Special' (special could be in an episode name? it's a tough one to consider)
            if (special == true)
            {
                if (fileString.Contains("OVA"))
                {
                    fileString = fileString.Replace("OVA", ""); ;
                    specialfile = true;
                }
                else if (fileString.Contains("SPECIAL"))
                {
                    fileString = fileString.Replace("SPECIAL", "");
                    specialfile = true;
                }
                else if (fileString.Contains("Special"))
                {
                    fileString = fileString.Replace("Special", "");
                    specialfile = true;
                }
                else specialfile = false;
            }
            else
            {
                specialfile = false;
            }

            string OriginalName = fileString; //Keep the original incase we detect an error with the input

            //LOGIC FOR RENAMING (RENAME CURRENT 0 INDEX ONLY)
            if (fileString.Contains(".mkv")) FileType = ".mkv";
            else if (fileString.Contains(".mp4")) FileType = ".mp4";

            //Remove anything in brackets, [horribleSubs] or [720p]
            var regex = new Regex("[[][^[]*[]]");
            fileString = (regex.Replace(fileString, String.Empty)).Trim();

            string test = null;
            int i = 0;//bit of a weird way to go about this
            //i = how many '-' are in the string
            //need to -1 one for the '- 01.mp4' part
            //also -1 for the exception one
            try
            {
                //the show name is //LOOP = CHECK FOR EXTRA '-'
                for (; ; i++) //Essentially loops until no more - are found and sticks that together to make episode name
                {
                    test = fileString.Split('-')[i]; //name of series is before the '-'
                    //test var is required to re-add extra '-'
                }
            }
            catch
            {
                i = i - 2;
                if (i < 0) SeriesName = OriginalName.Split('.')[0]; ; //There aren't any '-' so throw the name of the file to the series name - naughty file naming!
                for (int x = 0; x <= i; x++)
                {
                    test = fileString.Split('-')[x];
                    if (x > 0)
                    {
                        SeriesName = SeriesName + " - " + test.Trim();
                    }
                    else
                    {
                        SeriesName = test.Trim();
                    }
                }
            }

            if (LastSeriesName != SeriesName) numberAdjust = false; //return to default state

            //the ep number is
            try
            {

                EpisodeNo = fileString.Remove(fileString.IndexOf(SeriesName), SeriesName.Length);
                EpisodeNo = EpisodeNo.Replace('-', ' ');
                EpisodeNo = EpisodeNo.Split('.')[0];
                EpisodeNo = EpisodeNo.Trim();

                if (EpisodeNo.Length > 3) //it may already be SxEx foramtted, lets check
                {
                    EpisodeNo = Regex.Replace(EpisodeNo, @"[\d-]", string.Empty);
                    if (EpisodeNo.ToLower() == "se")
                    {
                        //contains S-E-, or at least something like that so assume it is.
                        SeriesName = SeriesName.Trim();
                        fileString = fileString.Trim();
                        validFileName = false; //It's not a valid file name because we dont want to use rename logic (cannot be applied unless in correct format)

                        return fileString;
                    }
                }
                SeriesName = SeriesName.Trim();
                fileString = fileString.Trim();

                //Checking if the episode number is unusually high, in rare occasions 'SERIESNAME Season two - 55" the episode number is carried on instead of reset for the new season
                //--If the parse fails, then it's not a number so something is wrong!
                //Need to save it here so that the user doesnt get asked the same thing about the next file which shares the same series name
                int intEpisodeNo = 0;
                intEpisodeNo = Int32.Parse(EpisodeNo);
                if (intEpisodeNo > 25 && LastSeriesName != SeriesName)
                {
                    //Ask the user if this is correct, it could be - so we need to make sure instead of assuming
                    DialogResult dialogResult = MessageBox.Show("The episode number of the file:\n\n            " + OriginalName + 
                        "\n\nIs pretty high. Should it be changed? (Is it the correct episode for this specific season?)", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes) //Time for shifting folders :(
                    {
                        numberAdjust = true;
                    }
                    else { numberAdjust = false; }
                }

                validFileName = true;
                return fileString;
            }         

            catch
            { //Formatting of the name is incorrect. Return the original
                validFileName = false;
                SeriesName = OriginalName;
                return OriginalName;
            }
        }

        List<int> SmartFolderSearch(List<string> SeriesFolder)
        {

            //Will get pretty complicated, the idea is that we do the most likely checks first
            //then if it's still not found, get gradually more complex (searching if there is something very similiar etc)
            List<int> Marker = new List<int>(); //Marks the location in the series folder list, in which the found folder is

            int i = 0;

            for (; i < SeriesFolder.Count; i++)
            {
                var regex = new Regex(@" ?\(.*?\)");
                string Checker = (regex.Replace(SeriesFolder[i], String.Empty)).Trim().ToLower();//used to make sure that caps do not matter with contains

                if (Checker.Contains(SeriesName.ToLower()))
                {
                    FolderCount++; //A file is found (or multiple) so we can pass the folder check in the next block IF
                    Marker.Add(i);
                }

            }

            if (FolderCount == 0) //No folders found, perhaps there is one almost identical that we should be using, but contains non-character symbols (~ instead of -? and such)
            {
                char[] arr = SeriesName.ToCharArray();
                arr = Array.FindAll<char>(arr, (c => (char.IsLetter(c)
                                                  || char.IsWhiteSpace(c))));
                string SeriesEdit = new string(arr);
                

                for (; i < SeriesFolder.Count; i++)
                {
                    //Try removing everything but spaces and letters to see if they match
                    char[] t = SeriesFolder[i].ToCharArray();
                    t = Array.FindAll<char>(t, (c => (char.IsLetter(c)
                                                      || char.IsWhiteSpace(c))));
                    string FolderEdit = new string(t);

                    if (FolderEdit == SeriesEdit) //If they are the same with numbers and stuff like '-' or ~ then this is likely the correct folder
                    {                             //Since we havent found one previously, lets just assume it is (may need to become smarter in the future, but lets try it for now)
                        FolderCount++;
                        Marker.Add(i);
                        //We should probably also set the name of the series to that of the folder, so it looks organized in the folder
                        SeriesName = SeriesFolder[i];
                    }

                }
            }

            if (FolderCount == 0) //STILL no folders??????? ok, let's check if there are any SIMILIAR named ones (some 2nd seasons add a litle bit to the string at the end)
            {
                if (SeriesName.Length >  7) //Could change to lower? but the problem is VERY small strings may
                {
                    i = 0;
                    for (; i < SeriesFolder.Count; i++)
                    {
                        if (SeriesName.StartsWith(SeriesFolder[i]))
                        {
                            DialogResult dialogResult = MessageBox.Show("No Folder Exists with the name: " + SeriesName + "\r\n\r\nHowever there is a folder called: " + SeriesFolder[i] + "\r\n\r\nDo you want to use this folder instead?", "", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                FolderCount++;
                                Marker.Add(i);
                                //We should probably also set the name of the series to that of the folder, so it looks organized in the folder
                                SeriesName = SeriesFolder[i];
                                break; //Because the folder has been manually chosen, we are always using this folder no matter what so we dont need to search for any more
                            }
                        }

                    }
                }

            }
            return Marker;
        }

        #region TEST METHODS
        void tests(string downloadLocation, string endLocation)
        {
            //Create test files to move
            downloadLocation = Application.StartupPath + "\\TestDownload\\";
            endLocation = Application.StartupPath + "\\TestVideos\\";

            Directory.CreateDirectory(downloadLocation);
            Directory.CreateDirectory(endLocation + "Test3\\" + "Season 1\\");
            Directory.CreateDirectory(endLocation + "Test4\\" + "Season 1\\"); //Create "SERIES" folders in videos directory
            Directory.CreateDirectory(endLocation + "Test4\\" + "Season 2\\");
            Directory.CreateDirectory(endLocation);

            for (int i = 1; i < 5; i++)
            {
                using (FileStream MP4 = File.Create(downloadLocation + "Test" + i + ".mp4")) //MP4 TEST
                using (FileStream MKV = File.Create(downloadLocation + "Test" + i + ".mkv")) //MKV TEST
                using (FileStream INVALID = File.Create(downloadLocation + "Test" + i)) //STANDARD FILE TEST (SHOULD NOT TRANSFER)
                using (FileStream NUMBERING = File.Create(downloadLocation + "Test" + i + " - " + i + ".mp4")) //NUMBERING TEST
                {

                }
            }


        }
        #endregion
    }


}

