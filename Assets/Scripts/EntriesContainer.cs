using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("Container")]
public class EntriesContainer
{
    [XmlArray("ArGpsLocationData")]
    public List<Entry> entriesContainer = new List<Entry>();
}