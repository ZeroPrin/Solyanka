using System;
using System.Collections;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class AccessAPI : MonoBehaviour
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string _urlCurrency = "https://www.cbr.ru/scripts/XML_daily.asp";

    private void Start()
    {
        //string rates = await GetCurrencyRates();

        //if (rates != null)
        //{
        //    Debug.Log(rates); 
        //}

        StartCoroutine(GetCurrencyRatesCoroutine());
    }

    public async Task<string> GetCurrencyRates()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_urlCurrency);
            response.EnsureSuccessStatusCode(); // ѕровер€ем успешность ответа

            // „тение содержимого ответа как строки
            string responseData = await response.Content.ReadAsStringAsync();
            return responseData; // ¬озвращаем данные курсов валют
        }
        catch (HttpRequestException e)
        {
            Debug.LogError($"ќшибка HTTP: {e.Message}");
            return null; // ¬ случае ошибки возвращаем null
        }
    }

    private IEnumerator GetCurrencyRatesCoroutine()
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(_urlCurrency); 
        // »спользование using гарантирует, что ресурс будет корректно освобожден после завершени€ работы, предотвраща€ утечки пам€ти.

        webRequest.timeout = 5;
        //”станавливает таймаут дл€ запроса на 5 секунд. ≈сли ответ от сервера не будет получен за это врем€, запрос будет прерван.

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received: " + webRequest.downloadHandler.text);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(webRequest.downloadHandler.text);
            // »спользуетс€ XmlDocument дл€ разбора XML-ответа.

            CurrencyInfo currencyInfo = new CurrencyInfo();

            XmlNode usdNode = xmlDoc.SelectSingleNode("//Valute[CharCode='USD']/Value");
            //ѕоиск значени€ дл€ доллара —Ўј в XML-документе происходит с помощью метода SelectSingleNode, который использует XPath Ч €зык запроса дл€ XML

            if (usdNode != null)
            {
                currencyInfo.USD = float.Parse(usdNode.InnerText.Replace(',', '.'), CultureInfo.InvariantCulture);
                // InnerText Ч это свойство XmlNode, которое возвращает текст содержимого этого узла.
                // ¬ данном случае это будет строка, содержаща€ значение курса доллара, например, "102,8078".

                // Replace(',', '.') замен€ет зап€тую в строке на точку. Ёто необходимо, потому что в некоторых культурах
                // (например, в –оссии) зап€та€ используетс€ как дес€тичный разделитель, в то врем€ как в большинстве англо€зычных стран (в том числе в C#) используетс€ точка.
                //ѕример:"102,8078" станет "102.8078"

                //ћетод float.Parse преобразует строку в число с плавающей точкой (тип float).

                //Ёто параметр, указывающий метод Parse, как интерпретировать строку. CultureInfo.InvariantCulture используетс€, чтобы гарантировать,
                //что точка используетс€ в качестве дес€тичного разделител€, независимо от настроек культуры системы.

                Debug.Log("USD Rate: " + currencyInfo.USD);
            }

            XmlNode bynNode = xmlDoc.SelectSingleNode("//Valute[CharCode='BYN']/Value");
            if (bynNode != null)
            {
                currencyInfo.BYN = float.Parse(bynNode.InnerText.Replace(',', '.'), CultureInfo.InvariantCulture);
                Debug.Log("BYN Rate: " + currencyInfo.BYN);
            }
            else
            {
                Debug.Log("BYN not found"); 
            }
        }
        else
        {
            Debug.Log("Error: " + webRequest.error);
        }
    }

    [Serializable]
    public class CurrencyInfo
    {
        public float USD;
        public float BYN;
    }
}
