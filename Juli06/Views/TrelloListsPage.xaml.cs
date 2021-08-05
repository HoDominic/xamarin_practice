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
    public partial class TrelloListsPage : ContentPage
    {

        public TrelloBoard MyBoard { get; set; }


        //via de ctor wordt het geselecteerde board toegevoegd
        public TrelloListsPage(Models.TrelloBoard selected)
        {
            InitializeComponent();

            //bijhouden 1:49

            MyBoard = selected;

            // lists van doorgegeven board gaan ophalen en tonen
            showListsFromBoard();
        }


        private async void  showListsFromBoard()
        {

            List<TrelloList> trellolists = await TrelloRepository.GetListsAsync(MyBoard.Id);
            //weergeven in de juiste listView
            lvwTrelloLists.ItemsSource = trellolists;
        }


        //bij het selecteren vane een TrelloList
        private void lvwTrelloLists_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(lvwTrelloLists.SelectedItem != null)
            {
                //verwerken
                TrelloList trelloListSelected = (TrelloList)lvwTrelloLists.SelectedItem;
                Navigation.PushAsync(new TrelloCardsPage(MyBoard, trelloListSelected));

                //deselecteren van het item
                lvwTrelloLists.SelectedItem = null;



            }
        }
    }
}