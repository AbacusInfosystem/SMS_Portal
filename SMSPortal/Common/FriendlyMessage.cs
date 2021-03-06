﻿using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Common
{
	public class FriendlyMessage
	{
		public FriendlyMessage()
		{

		}

		public FriendlyMessage(string code, MessageType type, string text)
		{
			this.Code = code;

			this.Type = type;

			this.Text = text;
		}

		public string Code
		{
			get;
			set;
		}

		public MessageType Type
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		public int TextLength
		{
			get
			{
				return Text.Length;
			}
		}
	}
}
