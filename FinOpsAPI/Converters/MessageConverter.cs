using FinOpsAPI.Models;
using Newtonsoft.Json;
using System.Xml;

namespace FinOpsAPI.Converters;

public class MessageConverter
{
    public static string GetXmlNodeValue(string xml)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);

        Root root = new Root
        {
            DocumentUniqueIdentifier = xmlDoc.SelectSingleNode("/Root/DocumentUniqueIdentifier").InnerText,
            OriginalDocumentGuid = xmlDoc.SelectSingleNode("/Root/OriginalDocumentGuid").InnerText,
            Comment = xmlDoc.SelectSingleNode("/Root/Comment").InnerText,
            Version = xmlDoc.SelectSingleNode("/Root/Version").InnerText,
            ResponseDateTime = xmlDoc.SelectSingleNode("/Root/ResponseDateTime").InnerText,
            Attachments = new Attachments
            {
                Attachment = new List<Attachment?>()
            }
        };

        XmlNodeList attachments = xmlDoc.SelectNodes("/Root/Attachments/Attachment");

        if (attachments != null)
        {
            foreach (XmlNode attachmentNode in attachments)
            {
                Attachment attachment = new Attachment
                {
                    FileName = attachmentNode.SelectSingleNode("FileName")?.InnerText,
                    Length = int.Parse(attachmentNode.SelectSingleNode("Length")?.InnerText ?? "0"),
                    BrokenFilesInfo = new BrokenFilesInfo 
                    {
                        BrokenFileInfo = new List<BrokenFileInfo?>()
                    }
                };

                XmlNodeList brokenFiles = attachmentNode.SelectNodes("BrokenFilesInfo/BrokenFileInfo");

                if (brokenFiles != null)
                {
                    foreach (XmlNode brokenFileNode in brokenFiles)
                    {
                        BrokenFileInfo brokenFileInfo = new BrokenFileInfo
                        {
                            Name = brokenFileNode.SelectSingleNode("Name")?.InnerText,
                            Length = int.Parse(brokenFileNode.SelectSingleNode("Length")?.InnerText ?? "0"),
                            Buffer = brokenFileNode.SelectSingleNode("Buffer")?.InnerText
                        };
                        attachment.BrokenFilesInfo.BrokenFileInfo.Add(brokenFileInfo);
                    }
                }

                root.Attachments.Attachment.Add(attachment);
            }
        }

        return JsonConvert.SerializeObject(root, Newtonsoft.Json.Formatting.Indented);
    }
}
