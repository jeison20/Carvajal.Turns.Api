using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Domain.Entities
{
	[Serializable]
	public class Attachment : IXmlSerializable
	{
		private string name;
		private string contentType;
		private long length;
		public string Name
		{
			get
			{
				return this.name;
			}
			private set
			{
				this.name = value;
			}
		}
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
			private set
			{
				this.contentType = value;
			}
		}
		public long Length
		{
			get
			{
				return this.length;
			}
			private set
			{
				this.length = value;
			}
		}
		public Attachment(string name)
		{
			Attachment.ValidateName(name);
			FileInfo fileInfo = new FileInfo(name);
			this.Name = name;
			this.ContentType = MimeType.Get(fileInfo.Extension);
			this.Length = fileInfo.Length;
		}
		private Attachment()
		{
		}
		public XmlSchema GetSchema()
		{
			return null;
		}
		public void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				this.Name = reader.ReadElementString("Name");
				this.ContentType = reader.ReadElementString("ContentType");
				this.Length = Convert.ToInt64(reader.ReadElementString("Length"));
				reader.ReadEndElement();
			}
		}
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString("Name", this.Name);
			writer.WriteElementString("ContentType", this.ContentType);
			writer.WriteElementString("Length", this.Length.ToString(CultureInfo.InvariantCulture));
		}
		private static void ValidateName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException("The parameter 'Name' cannot be an empty string.");
			}
			if (!File.Exists(name))
			{
				throw new FileNotFoundException(string.Format("Could not find file '{0}'", name));
			}
		}
	}
}
