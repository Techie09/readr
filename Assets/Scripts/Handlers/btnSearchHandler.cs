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
                    for (int index = 0; index < searchResult.ResultDetails.Count; ++index)
                    {
                        var detail = searchResult.ResultDetails[index];

                        GameObject g = new GameObject($"detail{index}");
                        Text txtDetailResult = g.AddComponent<Text>();
                        txtDetailResult.supportRichText = true;

                        var sb = new StringBuilder();
                        var line = String.Join(" ", detail.ResultRegion.Lines.Select(t => t.Text));
                        sb.AppendLine($"{line}"); //.Replace($"",$"<b>{detail.Text}</b>"));
                        sb.AppendLine();
                        sb.AppendLine($"Language: {detail.Language} | Line: {detail.LineNumber} | Position: {detail.Position} ");

                        txtDetailResult.text = sb.ToString();
                        txtDetailResult.rectTransform.SetParent(parent);
                        sb = null;
                    }
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
