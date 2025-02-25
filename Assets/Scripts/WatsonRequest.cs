using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatsonRequest
{
   public string requestName;
   public string text;

   public WatsonRequest(string requestText, string name)
   {
      requestName = name;
      text = requestText;
   }

}
