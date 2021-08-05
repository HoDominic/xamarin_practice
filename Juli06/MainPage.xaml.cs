using Juli06.Models;
using Juli06.Repositories;
using Juli06.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Juli06
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           // TestModels();
            LoadBoards();
        }
        

        private async void LoadBoards()
        {
            lvwBoards.ItemsSource = await TrelloRepository.GetBoards();

        }


        private async void TestModels()
        {
            Debug.WriteLine("Test models");
            List<TrelloBoard> trelloBoards = await TrelloRepository.GetBoards();
            foreach(TrelloBoard b in trelloBoards)
            {
                Debug.WriteLine(b.Name);
            }

            //test2:we nemen als test het eerste bord

            TrelloBoard trelloBoard = trelloBoards[0];
            List<TrelloList>trelloLists = await TrelloRepository.GetListsAsync(trelloBoard.Id);
            foreach (TrelloList l in trelloLists)
            {
                Debug.WriteLine(l.Name);
            }



            //test3: 
            TrelloList trelloList = trelloLists[0];
            TrelloCard trelloCard = new TrelloCard() { Name = "Demo2020", IsClosed = false };
            await TrelloRepository.AddCardsAsync(trelloList.Id, trelloCard);


            //test4: ophalen vd card van een List
            
            List<TrelloCard>cards =  await TrelloRepository.GetCardsAsync(trelloList.Id);
            Debug.WriteLine($"Aantal Cards:{cards.Count}");
            TrelloCard firstCard = cards[0];



            //test4: updaten van trelloCard 21:25
            
            firstCard.Name = "Updated name";
            await TrelloRepository.UpdateCardsAsync(firstCard);


        }

        private void lvwBoards_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(lvwBoards.SelectedItem != null)
            {
                //er is een board geselecteerd   
                TrelloBoard selected = (TrelloBoard)lvwBoards.SelectedItem;
                //we gaan naar een andere pagina
                Navigation.PushAsync(new TrelloListsPage(selected));
                //selected item deselecteren  1:47
                lvwBoards.SelectedItem = null;  
            }
        }
    }
}
