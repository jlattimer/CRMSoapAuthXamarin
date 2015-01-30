using System;
using System.Drawing;
using System.Text;
using System.Xml;
using Foundation;
using UIKit;

namespace CRMSoapAuthXamarin
{
    public partial class RootViewController : UIViewController
    {
        public RootViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        partial void Login_TouchUpInside(UIButton sender)
        {
            string url = CrmUrl.Text;
            if (!url.EndsWith("/"))
                url += "/";
            string username = Username.Text;
            string password = Password.Text;

            CrmAuth auth = new CrmAuth();
            CrmAuthenticationHeader authHeader = auth.GetHeaderOnline(username, password, url);

            string id = CrmWhoAmI(authHeader, url);
            WhoAmI.Text = CrmGetUserName(authHeader, id, url);
        }

        private static string CrmWhoAmI(CrmAuthenticationHeader authHeader, string url)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<s:Body>");
            xml.Append("<Execute xmlns=\"http://schemas.microsoft.com/xrm/2011/Contracts/Services\">");
            xml.Append("<request i:type=\"c:WhoAmIRequest\" xmlns:b=\"http://schemas.microsoft.com/xrm/2011/Contracts\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:c=\"http://schemas.microsoft.com/crm/2011/Contracts\">");
            xml.Append("<b:Parameters xmlns:d=\"http://schemas.datacontract.org/2004/07/System.Collections.Generic\"/>");
            xml.Append("<b:RequestId i:nil=\"true\"/>");
            xml.Append("<b:RequestName>WhoAmI</b:RequestName>");
            xml.Append("</request>");
            xml.Append("</Execute>");
            xml.Append("</s:Body>");

            XmlDocument xDoc = CrmExecuteSoap.ExecuteSoapRequest(authHeader, xml.ToString(), url);
            if (xDoc == null)
                return null;

            XmlNodeList nodes = xDoc.GetElementsByTagName("b:KeyValuePairOfstringanyType");
            foreach (XmlNode node in nodes)
            {
                if (node.FirstChild.InnerText == "UserId")
                {
                    return node.LastChild.InnerText;
                }
            }

            return null;
        }

        private static string CrmGetUserName(CrmAuthenticationHeader authHeader, string id, string url)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<s:Body>");
            xml.Append("<Execute xmlns=\"http://schemas.microsoft.com/xrm/2011/Contracts/Services\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">");
            xml.Append("<request i:type=\"a:RetrieveRequest\" xmlns:a=\"http://schemas.microsoft.com/xrm/2011/Contracts\">");
            xml.Append("<a:Parameters xmlns:b=\"http://schemas.datacontract.org/2004/07/System.Collections.Generic\">");
            xml.Append("<a:KeyValuePairOfstringanyType>");
            xml.Append("<b:key>Target</b:key>");
            xml.Append("<b:value i:type=\"a:EntityReference\">");
            xml.Append("<a:Id>" + id + "</a:Id>");
            xml.Append("<a:LogicalName>systemuser</a:LogicalName>");
            xml.Append("<a:Name i:nil=\"true\" />");
            xml.Append("</b:value>");
            xml.Append("</a:KeyValuePairOfstringanyType>");
            xml.Append("<a:KeyValuePairOfstringanyType>");
            xml.Append("<b:key>ColumnSet</b:key>");
            xml.Append("<b:value i:type=\"a:ColumnSet\">");
            xml.Append("<a:AllColumns>false</a:AllColumns>");
            xml.Append("<a:Columns xmlns:c=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\">");
            xml.Append("<c:string>firstname</c:string>");
            xml.Append("<c:string>lastname</c:string>");
            xml.Append("</a:Columns>");
            xml.Append("</b:value>");
            xml.Append("</a:KeyValuePairOfstringanyType>");
            xml.Append("</a:Parameters>");
            xml.Append("<a:RequestId i:nil=\"true\" />");
            xml.Append("<a:RequestName>Retrieve</a:RequestName>");
            xml.Append("</request>");
            xml.Append("</Execute>");
            xml.Append("</s:Body>");

            XmlDocument xDoc = CrmExecuteSoap.ExecuteSoapRequest(authHeader, xml.ToString(), url);
            if (xDoc == null)
                return null;

            string firstname = "";
            string lastname = "";

            XmlNodeList nodes = xDoc.GetElementsByTagName("b:KeyValuePairOfstringanyType");
            foreach (XmlNode node in nodes)
            {
                if (node.FirstChild.InnerText == "firstname")
                {
                    firstname = node.LastChild.InnerText;
                }

                if (node.FirstChild.InnerText == "lastname")
                {
                    lastname = node.LastChild.InnerText;
                }
            }

            return firstname + " " + lastname;
        }
        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}