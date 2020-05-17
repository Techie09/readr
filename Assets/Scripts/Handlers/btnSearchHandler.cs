using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Readr.Assets.Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class btnSearchHandler : MonoBehaviour
{
    public InputField txtSearch;
    public VerticalLayoutGroup grpSearchResults;
    public GameObject ListItemPrefab;

    private UserSessionController _userSessionClient;

    public btnSearchHandler()
    {
        _userSessionClient = new UserSessionController();
    }

    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(() => SearchText());
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public async void SearchText()
    {
        Debug.Log("btnSearch clicked");

        //get text from input field
        var searchText = txtSearch.text;
        if(!String.IsNullOrWhiteSpace(searchText))
        {
            try
            {
                //send api call to Readr backend
                var searchResult = await _userSessionClient.SearchText(searchText);

                //if any results
                if(searchResult != null)
                {
                    //display resultssynthwave
                    RectTransform parent = grpSearchResults.GetComponent<RectTransform>();

                    //Destroy all the children
                    for(int i = 0; i < parent.childCount; ++i)
                    {
                        var child = parent.GetChild(i);
                        Destroy(child.gameObject);
                    }

                    searchResult.ResultDetails = searchResult.ResultDetails.OrderBy(r => r.LineNumber).ThenBy(p => p.Position).ToList();

                    for (int index = 0; index < searchResult.ResultDetails.Count; ++index)
                    {
                        var detail = searchResult.ResultDetails[index];

                        GameObject g = Instantiate(ListItemPrefab) as GameObject;
                        g.name = $"detail{index}";
                        g.SetActive(true);
                        SearchItem txtDetailResult = g.GetComponent<SearchItem>();
                        txtDetailResult.Title.richText = true;
                        var r = String.Join(" ", detail.ResultLine.Text.Select(t => t.Text));
                        Debug.Log($"number of words {detail.ResultLine.Text.Count}");
                        Debug.Log(r);
                        txtDetailResult.Title.text = r;

                        txtDetailResult.Description.richText = true;
                        txtDetailResult.Description.text = $"Line: {detail.LineNumber} | Position: {detail.Position} ";

                        var gRect = g.GetComponent<RectTransform>();
                        gRect.SetParent(parent);
                        gRect.SetAsLastSibling();
                    }
                    LayoutRebuilder.MarkLayoutForRebuild(parent);
                    LayoutRebuilder.ForceRebuildLayoutImmediate(parent);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                //Display Error message
            }
        }
    }
}
