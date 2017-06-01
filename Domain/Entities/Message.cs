using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Carvajal.Turns.Domain.Entities
{
	[Serializable]
	public class Message
	{
		private bool isCarbonCopy;
		private bool isBlindCarbonCopy;
		private List<Attachment> attachments;
		private Address from;
		private Address to;
		private string subject;
		private string body;
		private bool isBodyHtml;
		private string requestId;
		public Address From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}
		public Address To
		{
			get
			{
				return this.to;
			}
			set
			{
				this.to = value;
			}
		}
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}
		public bool IsBodyHtml
		{
			get
			{
				return this.isBodyHtml;
			}
			set
			{
				this.isBodyHtml = value;
			}
		}
		public string RequestId
		{
			get
			{
				return this.requestId;
			}
			set
			{
				this.requestId = value;
			}
		}
		public bool IsCarbonCopy
		{
			get
			{
				return this.isCarbonCopy;
			}
			set
			{
				if (value)
				{
					this.isCarbonCopy = true;
					this.isBlindCarbonCopy = false;
				}
			}
		}
		public bool IsBlindCarbonCopy
		{
			get
			{
				return this.isBlindCarbonCopy;
			}
			set
			{
				if (value)
				{
					this.isBlindCarbonCopy = true;
					this.isCarbonCopy = false;
				}
			}
		}
		public List<Attachment> Attachments
		{
			get
			{
				List<Attachment> arg_19_0;
				if ((arg_19_0 = this.attachments) == null)
				{
					arg_19_0 = (this.attachments = new List<Attachment>());
				}
				return arg_19_0;
			}
		}
	}
}
