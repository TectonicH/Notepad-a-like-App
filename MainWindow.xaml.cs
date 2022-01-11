using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Notepad_a_like_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // This will be used to keep track of any changes to the file
        bool recentlySaved = true;

        public MainWindow()
        {
            InitializeComponent();
        }


        /*  -- Method Header Comment
            Name	:   CloseFile
            Purpose :   This method will close the file and ask the user if they'd like to save
            Returns	:	
        */
        private void CloseFile(object sender, RoutedEventArgs e)
        {
            // If the document has not been recently saved...
            if (recentlySaved == false)
            {
                // Display a message box and store the users choice within a variable
                var closeBoxResult = WantToSaveMessageBox();
                // If they chose Yes or No, close the program
                if (closeBoxResult == MessageBoxResult.Yes || closeBoxResult == MessageBoxResult.No)
                {
                    this.Close();
                }
                else if (closeBoxResult == MessageBoxResult.Cancel)
                {

                }
            }
            else
            {
                this.Close();
            }
        }

        /*  -- Method Header Comment
    Name	:   NewFile
    Purpose :   This method will close the file and ask the user if they'd like to save or not
    Returns	:	
*/
        private void NewFile(object sender, RoutedEventArgs e)
        {
            // If the document has not been recently saved...
            if (recentlySaved == false)
            {
                // Display a message box and store the users choice within a variable
                var newBoxResult = WantToSaveMessageBox();
                // If they chose Yes or No, clear the text box
                if (newBoxResult == MessageBoxResult.Yes || newBoxResult == MessageBoxResult.No)
                {
                    txtEditor.Clear();
                }
                // Do nothing if they press cancel 
                else if (newBoxResult == MessageBoxResult.Cancel)
                {

                }
            }
        }

        /*  -- Method Header Comment
    Name	:   TxtEditor
    Purpose :   This method will count the number of chars the user types 
    Returns	:	
*/
        private void TxtEditor(object sender, TextChangedEventArgs e)
        {
            // This will keep track of the amount of characters as well as displaying "characters" after the number
            charCount.Content = txtEditor.Text.Length + " characters";
            // Every time the character count is changed, recently saved will be switched to false
            recentlySaved = false;
        }

        /*  -- Method Header Comment
           Name	:   OpenFile
           Purpose :   This method will open a file and ask if the user would like to save or not
           Returns	:	
       */
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // This will set the file name to default
            open.FileName = "default.txt";
            // We'll allow text files to be displayed
            open.Filter = "Text files| *.txt|All Files|*.*";

            // If the document has not been recently saved...
            if (recentlySaved == false)
            {
                // Display a message box and store the users choice within a variable
                var newBoxResult = WantToSaveMessageBox();
                // If they chose Yes or No, show the open file dialog
                if (newBoxResult == MessageBoxResult.Yes || newBoxResult == MessageBoxResult.No)
                {
                    if (open.ShowDialog() == true)
                    {
                        txtEditor.Text = File.ReadAllText(open.FileName);
                    }

                }
                else if (newBoxResult == MessageBoxResult.Cancel)
                {

                }
            }
            else
            {
                if (open.ShowDialog() == true)
                {
                    txtEditor.Text = File.ReadAllText(open.FileName);
                }
            }
        }

        /*  -- Method Header Comment
            Name	:   SaveFileAs
            Purpose :   This method will open a file and ask if the user would like to save or not.
                        It was use a message box to allow the user to save.  
            Returns	:	
        */
        private void SaveFileAs(object sender, RoutedEventArgs e)
        {
            LetsSaveTheFileAs();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        /*
            FUNCTION : WantToSaveMessageBox
            DESCRIPTION : This function will open a message box for the user and 
                          allow them to save if they want 
            RETURNS : 
        */
        private MessageBoxResult WantToSaveMessageBox()
        {
            // Here we'll ask if they'd like to save changes 
            string messageBoxText = "Would you like to save changes?";
            // This will be the name of the program
            string caption = "TextPad";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            switch (result)
            {
                // If they choose yes, the file will save
                case MessageBoxResult.Yes:
                    LetsSaveTheFileAs();
                    break;
            }
            // We return the result of their choice 
            return result;
        }

        /*
            FUNCTION : LetsSaveTheFileAs
            DESCRIPTION : This function will save the file as a text file
            RETURNS : 
        */
        private void LetsSaveTheFileAs()
        {
            SaveFileDialog saveAs = new SaveFileDialog();

            // We'll save as a text file here
            saveAs.Filter = "Text File (*.txt)|*.txt";
            if (saveAs.ShowDialog() == true)
            {
                File.WriteAllText(saveAs.FileName, txtEditor.Text);
                // As soon as we save the document, we change recentlysaved to true
                recentlySaved = true;
            }
        }

        /*  -- Method Header Comment
            Name	:   AboutProgram
            Purpose :   This method will open an about box displaying information about the program  
            Returns	:	
        */
        private void AboutProgram(object sender, RoutedEventArgs e)
        {
            // This will be the about box's text 
            string messageBoxText = " KristianSoft Doors\n Version 7KB2 (OS Build 31. 32)\n KristianSoft Corporation. " +
                "All rights reserved.\n\n This textpad will allow you to get all\n the work done that you ever wanted. \n You're welcome!";
            string caption = "About";
            MessageBoxButton button = MessageBoxButton.OK;
            var icon = MessageBoxImage.Information;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }
    }



}
