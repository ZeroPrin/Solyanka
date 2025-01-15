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
            response.EnsureSuccessStatusCode(); // ��������� ���������� ������

            // ������ ����������� ������ ��� ������
            string responseData = await response.Content.ReadAsStringAsync();
            return responseData; // ���������� ������ ������ �����
        }
        catch (HttpRequestException e)
        {
            Debug.LogError($"������ HTTP: {e.Message}");
            return null; // � ������ ������ ���������� null
        }
    }

    private IEnumerator GetCurrencyRatesCoroutine()
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(_urlCurrency); 
        // ������������� using �����������, ��� ������ ����� ��������� ���������� ����� ���������� ������, ������������ ������ ������.

        webRequest.timeout = 5;
        //������������� ������� ��� ������� �� 5 ������. ���� ����� �� ������� �� ����� ������� �� ��� �����, ������ ����� �������.

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received: " + webRequest.downloadHandler.text);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(webRequest.downloadHandler.text);
            // ������������ XmlDocument ��� ������� XML-������.

            CurrencyInfo currencyInfo = new CurrencyInfo();

            XmlNode usdNode = xmlDoc.SelectSingleNode("//Valute[CharCode='USD']/Value");
            //����� �������� ��� ������� ��� � XML-��������� ���������� � ������� ������ SelectSingleNode, ������� ���������� XPath � ���� ������� ��� XML

            if (usdNode != null)
            {
                currencyInfo.USD = float.Parse(usdNode.InnerText.Replace(',', '.'), CultureInfo.InvariantCulture);
                // InnerText � ��� �������� XmlNode, ������� ���������� ����� ����������� ����� ����.
                // � ������ ������ ��� ����� ������, ���������� �������� ����� �������, ��������, "102,8078".

                // Replace(',', '.') �������� ������� � ������ �� �����. ��� ����������, ������ ��� � ��������� ���������
                // (��������, � ������) ������� ������������ ��� ���������� �����������, � �� ����� ��� � ����������� ������������ ����� (� ��� ����� � C#) ������������ �����.
                //������:"102,8078" ������ "102.8078"

                //����� float.Parse ����������� ������ � ����� � ��������� ������ (��� float).

                //��� ��������, ����������� ����� Parse, ��� ���������������� ������. CultureInfo.InvariantCulture ������������, ����� �������������,
                //��� ����� ������������ � �������� ����������� �����������, ���������� �� �������� �������� �������.

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
