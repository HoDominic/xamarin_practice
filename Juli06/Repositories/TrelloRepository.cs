using Juli06.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Juli06.Repositories
{
    public class TrelloRepository
    {
        // my token and api key
        private const String _APIKEY = "3fc52c9031cbcf91cbb86c1c11c43255";
        private const string _Token = "c93f0fedecd2ddcec9266465af15d1bfcc90aa02e50e931e930c8d99427caa7c";


        // automatically add key and token to url 
        private static String AddKeyAndToken(string url)
        {
            return $"{url}?key={_APIKEY}&token={_Token}";
        }

        private static async Task<HttpClient> GetClient()
        {
            HttpClient httpClient = new HttpClient();
            //receive data in json format
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            return httpClient;
        }

        //just an api test
        //https://trello.com/1/members/my/boards?key=3fc52c9031cbcf91cbb86c1c11c43255&token=c93f0fedecd2ddcec9266465af15d1bfcc90aa02e50e931e930c8d99427caa7c





        //1:GET  TrelloBoards

        public async static Task<List<TrelloBoard>> GetBoards()
        {
            //HttpClient nodig --> tussenpersoon die zorgt voor API Call
            using (HttpClient client = await GetClient())
            {
                String url = AddKeyAndToken("https://trello.com/1/members/my/boards");
                String json = await client.GetStringAsync(url);

                if (json != null)
                {
                    // json --> list TrelloBoards

                    List<TrelloBoard> boards = JsonConvert.DeserializeObject<List<TrelloBoard>>(json);
                    return boards;

                }
                else
                {
                    return null;
                }



            }

        }

        //2: voor 1 trelloBoard de List opvragen

        public async static Task<List<TrelloList>> GetListsAsync(String boardId)
        {
            //HttpClient nodig --> tussenpersoon die zorgt voor API Call
            using (HttpClient client = await GetClient())
            {
                String url = AddKeyAndToken($"https://trello.com/1/boards/{boardId}/lists"); //Zie API
                String json = await client.GetStringAsync(url);

                if (json != null)
                {
                    // json --> list TrelloList

                    List<TrelloList> trelloList = JsonConvert.DeserializeObject<List<TrelloList>>(json);
                    return trelloList;

                }
                else
                {
                    return null;
                }

            }



        }



        //3: voor 1 trelloList de trellocards opvragen 


        public async static Task<List<TrelloCard>> GetCardsAsync(String listId)
        {
            //HttpClient nodig --> tussenpersoon die zorgt voor API Call
            using (HttpClient client = await GetClient())
            {
                try { 
                String url = AddKeyAndToken($"https://trello.com/1/lists/{listId}/cards"); //Zie API
                String json = await client.GetStringAsync(url);

                if (json != null)
                {
                    // json --> list TrelloList

                    List<TrelloCard> trelloCard = JsonConvert.DeserializeObject<List<TrelloCard>>(json);
                    return trelloCard;

                }
                else
                {
                    return null;
                }
                } catch(Exception ex)
                {
                    Debug.WriteLine("Fout bij opvragen van trelloCard");
                    throw ex;
                }

            }



        }

        //4: Add new card


        public async  static Task AddCardsAsync(String listId, TrelloCard newCard)
        {
            //deze methode voegt een nieuwe TrelloCard toe aan de list (listId als parameter)
            using (HttpClient client = await GetClient())
            {
                try { 
                // er mag maar 1 vraagteken in uw url
                string url = $"https://trello.com/1/cards?idList={listId}&key={_APIKEY}&token={_Token}";
                //stap2 card moet meegestuurd worden met url
                //kaart --> json
                String json = JsonConvert.SerializeObject(newCard);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url,content);

                //controle: is het gelukt?
                if (response.IsSuccessStatusCode == false)
                {

                    throw new Exception("Toevoegen van card niet geslaagd");
                }
                }catch(Exception ex)
                {
                    throw ex;
                }



            }

            }


        //update card
        public async static  Task UpdateCardsAsync(TrelloCard updatedCard)
        {
            //deze methode voegt een nieuwe TrelloCard toe aan de list (listId als parameter)
            using (HttpClient client = await GetClient())
            {
                try
                {
                    // er mag maar 1 vraagteken in uw url
                    string url =AddKeyAndToken($"https://trello.com/1/cards/{updatedCard.Id}");
                    //stap2 card moet meegestuurd worden met url
                    //kaart --> json
                    String json = JsonConvert.SerializeObject(updatedCard);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(url, content);

                    //controle: is het gelukt?
                    if (response.IsSuccessStatusCode == false)
                    {

                        throw new Exception("Updaten van card niet geslaagd");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }



            }
        }

        //6: ophalen van 1 trelloCard op basis van id
        public async static Task<TrelloCard> GetTrelloCardByIdAsync(String cardId) 
        {
            using (HttpClient client = await GetClient())
            {
                try
                {
                    String url = AddKeyAndToken($"https://trello.com/1/cards/{cardId}"); //Zie API
                    String json = await client.GetStringAsync(url);

                    if (json != null)
                    {
                        // json --> list TrelloList

                        TrelloCard trelloCard = JsonConvert.DeserializeObject <TrelloCard>(json);
                        return trelloCard;

                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fout bij opvragen van trelloCard");
                    throw ex;
                }

            }




        }









    }
}
