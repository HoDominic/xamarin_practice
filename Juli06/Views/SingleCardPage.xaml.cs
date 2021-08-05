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
    public partial class SingleCardPage : ContentPage
    {
        //info bijhouden via Properties 54 vid2
        public TrelloBoard  MyBoard { get; set; }

        public TrelloList MyList { get; set; }
        public SingleCardPage(TrelloBoard trelloBoard, TrelloList trelloList)
        {
            InitializeComponent();

            MyBoard = trelloBoard;
            MyList = trelloList;

            //labels opvullen
            lblBoard.Text = MyBoard.Name;
            lblList.Text = MyList.Name;
            Title = "Add a new card";
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            //opslaan van een  nieuwe trelloCard
            String name = editName.Text;

            TrelloCard newTrelloCard = new TrelloCard();
            newTrelloCard.Name = name;

            await TrelloRepository.AddCardsAsync(MyList.Id, newTrelloCard);

            Navigation.PopAsync();
        }
    }
}