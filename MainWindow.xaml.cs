//Anthony Franklin afranklin18@cnm.edu
//FranklinP8
//04/23/2022
//MainWindow

using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FranklinP8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BindingList<Card> cards;
        Card card;
        DBManager dbManager;
        public MainWindow()
        {
            InitializeComponent();
            cards = new BindingList<Card>();
            lbxCards.ItemsSource = cards;
            dbManager = new DBManager();
            dbManager.GetCards(cards);
            if(cards.Count > 0)
            {
                card = GetRandomCard();
                DisplayCardQuestion();
            }
        }

        private void DisplayCardQuestion()
        {
            txbAnswerContent.Text = "";
            txbQuestionContent.Text = card.Question;
            lblInstructions.Content = "Think about your answer and when ready click Show Answer";
        }

        private Card GetRandomCard()
        {
            Random random = new Random();
            int rand = random.Next(cards.Count);
            return cards[rand];
        }

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {
            txbAnswerContent.Text = card.Answer;
            lblInstructions.Content = "If your answer was correct, click Right. If not, click Wrong";
            btnRight.Visibility = Visibility.Visible;
            btnWrong.Visibility = Visibility.Visible;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(txbTitle.Text != "" && txbAnswer.Text != "" && txbQuestion.Text != "")
            {
                card = new Card(0, 0, 0, txbTitle.Text, txbAnswer.Text, txbQuestion.Text);
                dbManager.AddCard(card);
                dbManager.GetCards(cards);
                lbxCards.Items.Refresh();
                lblCardID.Content = "";
                txbTitle.Text = "";
                txbAnswer.Text = "";
                txbQuestion.Text = "";
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lbxCards.SelectedItem != null)
            {
                card = lbxCards.SelectedItem as Card;
                card.Title = txbTitle.Text;
                card.Question = txbQuestion.Text;
                card.Answer = txbAnswer.Text;
                dbManager.Update(card);
                dbManager.GetCards(cards);
                lbxCards.Items.Refresh();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lbxCards.SelectedItem != null)
            {
                card = lbxCards.SelectedItem as Card;
                dbManager.RemoveCard(card);
                dbManager.GetCards(cards);
                lbxCards.Items.Refresh();
                lblCardID.Content = "";
                txbTitle.Text = "";
                txbAnswer.Text = "";
                txbQuestion.Text = "";
            }

        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            card.NumRight++;
            dbManager.Update(card);
            dbManager.GetCards(cards);
            lbxCards.Items.Refresh();
            card = GetRandomCard();
            DisplayCardQuestion();
            btnRight.Visibility = Visibility.Hidden;
            btnWrong.Visibility = Visibility.Hidden;
            lblInstructions.Content = "";
        }

        private void btnWrong_Click(object sender, RoutedEventArgs e)
        {
            card.NumWrong++;
            dbManager.Update(card);
            dbManager.GetCards(cards);
            lbxCards.Items.Refresh();
            card = GetRandomCard();
            DisplayCardQuestion();
            btnRight.Visibility = Visibility.Hidden;
            btnWrong.Visibility = Visibility.Hidden;
        }

        private void lbxCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            card = lbxCards.SelectedItem as Card;
            if(card != null)
            {
                lblCardID.Content = card.CardID;
                txbTitle.Text = card.Title;
                txbAnswer.Text = card.Answer;
                txbQuestion.Text = card.Question;
            }
        }

        private void onTabSelect(object sender, SelectionChangedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if(ti.Name == "tabFlashCard")
            {
                card = GetRandomCard();
                DisplayCardQuestion();
            }
        }
    }
}
