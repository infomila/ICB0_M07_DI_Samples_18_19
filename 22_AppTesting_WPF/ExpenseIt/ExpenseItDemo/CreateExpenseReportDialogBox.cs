// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;
using System.Windows.Controls;

namespace ExpenseItDemo
{
    /// <summary>
    ///     Interaction logic for CreateExpenseReportDialogBox.xaml
    /// </summary>
    public partial class CreateExpenseReportDialogBox : UserControl
    {
        public CreateExpenseReportDialogBox()
        {
            InitializeComponent();
        }

        private void addExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            int amount;
            bool isNumber = System.Int32.TryParse(txtNewAmount.Text, out amount);

            if (cboExpense.SelectedIndex >= 0 &&
                txtNewType.Text.Trim().Length > 2 &&
                isNumber) { 
                var app = Application.Current;
                var expenseReport = (ExpenseReport)app.FindResource("ExpenseData");
                LineItem line = new LineItem(txtNewType.Text, cboExpense.Text,  amount);
               
                expenseReport?.LineItems.Add(line);
                expenseReport?.OnLineItemCostChanged(this, null);
            }
        }

        private void viewChartButton_Click(object sender, RoutedEventArgs e)
        {
    
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Expense Report Created!",
                "ExpenseIt Standalone",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

         }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
         }
    }
}