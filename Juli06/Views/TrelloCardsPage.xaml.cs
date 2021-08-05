using Juli06.Models;
using Juli06.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Juli06.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrelloCardsPage : ContentPage
    {
        //info bijhouden van properties


        public TrelloBoard MyBoard { get; set; }

        public TrelloList MyList { get; set; }



        public TrelloCardsPage(Models.TrelloBoard trelloBoard, Models.TrelloList trelloList)
        {
            InitializeComponent();

            //info te bewaren
            MyBoard = trelloBoard;
            MyList = trelloList;
            

            //label goed zetten (close)
            lblListName.Text = MyList.Name;

            //we voegen een Gesture toe aan een label: wanneer er op tap 
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += TapGesture_Tapped;
            lblAddCard.GestureRecognizers.Add(tapGesture);

        }


        //deze methode word elke keer uitgevoerd wanneer de pagina terug tevoorschijn komt
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCards();
        }


        private void TapGesture_Tapped(object sender, EventArgs e)//1:03
        {
            Navigation.PushAsync(new SingleCardPage(MyBoard, MyList));
        }

        private async  void LoadCards() //38 08 video2
        {
            await TrelloRepository.GetCardsAsync(MyList.Id);

            List<TrelloCard> trelloCards = await TrelloRepository.GetCardsAsync(MyList.Id);
            lvwCards.ItemsSource = trelloCards;


            
        }


        private void btnGoBack_Clicked(object sender, EventArgs e)
        {
            // ga terug naar de vorige page
            Navigation.PopAsync();
        }



        // deze methode wordt uitgevoed bij het klikken van een close-button van één van de cards

        private async void btnCloseCard_Clicked(object sender, EventArgs e)
        {
            // welke close button? er bestaan er meerdere..
            TrelloCard card = (sender as Button).BindingContext as TrelloCard;
            card.IsClosed = true;
            // updaten van card via TrelloRepository
            await TrelloRepository.UpdateCardsAsync(card);
            //load again
            LoadCards();
            
        }
    }
}