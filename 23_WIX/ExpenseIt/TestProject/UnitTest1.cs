using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

namespace TestProject
{
    [TestClass]
    public class UITests
    {
        [TestMethod]
        public void TestNumber1()
        {
            // ruta a l'executable de l'aplicació
            string ruta = System.AppDomain.CurrentDomain.BaseDirectory;
            ruta += @"\..\..\..\ExpenseItDemo\bin\Debug\ExpenseItDemo.exe";
            // a partir de la ruta, executem el programa
            Application app = Application.Launch(ruta);

            Window w = app.GetWindows()[0];

            // AutomationID = identificador d'un objecte

            // Prova de email erroni
            TextBox email = w.Get<TextBox>("emailTextBox");
            email.Text = "cacademail";
            Button b = w.Get<Button>("createExpenseReportButton");
            b.Click();
            Label emailError = w.Get<Label>("emailTextBoxErr");
            Label numberError = w.Get<Label>("employeeNumberTextBoxErr");
            Label employeeError = w.Get<Label>("lsbEmployeesErr");

            // Veriquem que tots els camps donen error
            Assert.AreEqual(email.Text + " is not a valid email address.", emailError.Text);
            Assert.AreEqual(" is not a number.", numberError.Text);
            Assert.AreEqual("Select an employee", employeeError.Text);

            // Prova de email vàlid
            email.Text = "mailcorrecte@gmail.com";
            b.Click();
            Assert.AreEqual( "", emailError.Text);
            //-------------------------------------------------------
            //lsbEmployees
            ListBox lb = w.Get<ListBox>("lsbEmployees");
            lb.Items[1].Click();
            b.Click();
            Assert.AreEqual("", employeeError.Text);

            //-------------------------------------------------------
            // Provem el número d'empleat
            TextBox employeeTextBox = w.Get<TextBox>("employeeNumberTextBox");
            employeeTextBox.Text = "a";
            b.Click();
            Assert.AreEqual(employeeTextBox.Text + " is not a number.", numberError.Text);
            employeeTextBox.Text = "1234";
            b.Click();
            Assert.AreEqual("Number must be 5 digits long.", numberError.Text);
            employeeTextBox.Text = "12345";
            b.Click();
            Assert.AreEqual("", numberError.Text);
            //------------------------------------------
            try
            {
                TextBox aliasTextbox = w.Get<TextBox>("aliasTextBox");
            }catch(Exception ex)
            {
                Assert.Fail("No s'ha obert la finestra de report.");
            }
        }
    }
}
