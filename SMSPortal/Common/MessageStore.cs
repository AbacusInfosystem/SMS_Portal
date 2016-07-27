using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortal.Common
{
	public class MessageStore
	{
		public static FixedSizeGenericHashTable<string, FriendlyMessage> hash = new FixedSizeGenericHashTable<string, FriendlyMessage>(400);

		static MessageStore()
		{
			#region System

			FriendlyMessage SYS01 = new FriendlyMessage("SYS01", MessageType.Error, "We are currently unable to process your request, Please try again later or contact system administrator.");
			hash.Add("SYS01", SYS01);

            FriendlyMessage SYS02 = new FriendlyMessage("SYS02", MessageType.Error, "Your session has expired. Please login again.");
			hash.Add("SYS02", SYS02);

			FriendlyMessage SYS03 = new FriendlyMessage("SYS03", MessageType.Error, "Please login with valid Username & Password to view this page.");
			hash.Add("SYS03", SYS03);

			FriendlyMessage SYS04 = new FriendlyMessage("SYS04", MessageType.Information, "No records found.");
			hash.Add("SYS04", SYS04);

			FriendlyMessage SYS05 = new FriendlyMessage("SYS05", MessageType.Information, "Password has been changed successfully. You can use your new password to login.");
			hash.Add("SYS05", SYS05);

			FriendlyMessage SYS06 = new FriendlyMessage("SYS06", MessageType.Error, "You dont have online access. Please contact administrator.");
			hash.Add("SYS06", SYS06);

			FriendlyMessage SYS07 = new FriendlyMessage("SYS07", MessageType.Error, "File not found, Please contact administrator.");
			hash.Add("SYS07", SYS07);

            FriendlyMessage SYS08 = new FriendlyMessage("SYS08", MessageType.Error, "File not found, Please contact administrator.");
            hash.Add("SYS08", SYS08);

            FriendlyMessage SYS09 = new FriendlyMessage("SYS09", MessageType.Information, "Link has expired.");
            hash.Add("SYS09", SYS09);

            FriendlyMessage SYS010 = new FriendlyMessage("SYS010", MessageType.Information, "You have successfully logged out!");
            hash.Add("SYS010", SYS010);

			#endregion

            #region Category 

            FriendlyMessage RO001 = new FriendlyMessage("CO001", MessageType.Success, "Category added successfully");
            hash.Add("CO001", RO001);

            FriendlyMessage RO002 = new FriendlyMessage("CO002", MessageType.Success, "Category updated successfully");
            hash.Add("CO002", RO002);

            FriendlyMessage RO003 = new FriendlyMessage("CO003", MessageType.Success, "Category deleted successfully");
            hash.Add("CO003", RO003);

            #endregion

            #region Brands

            FriendlyMessage BO001 = new FriendlyMessage("BO001", MessageType.Success, "Brand added successfully");
            hash.Add("BO001", BO001);

            FriendlyMessage BO002 = new FriendlyMessage("BO002", MessageType.Success, "Brand updated successfully");
            hash.Add("BO002", BO002);

            FriendlyMessage BO003 = new FriendlyMessage("BO003", MessageType.Success, "Brand deleted successfully");
            hash.Add("BO003", BO003);

            FriendlyMessage BO004 = new FriendlyMessage("BO004", MessageType.Success, "Brand logo uploaded successfully");
            hash.Add("BO004", BO004);

            FriendlyMessage BO005 = new FriendlyMessage("BO005", MessageType.Success, "Brand logo changed successfully");
            hash.Add("BO005", BO005);

            FriendlyMessage BO006 = new FriendlyMessage("BO006", MessageType.Success, "Brand profile updated successfully");
            hash.Add("BO006", BO006);

            #endregion

            #region User

            FriendlyMessage UM001 = new FriendlyMessage("UM001", MessageType.Success, "User added successfully");
            hash.Add("UM001", UM001);

            FriendlyMessage SO002 = new FriendlyMessage("UM002", MessageType.Success, "User updated successfully");
            hash.Add("UM002", SO002);

            FriendlyMessage SO003 = new FriendlyMessage("UM003", MessageType.Success, "User deleted successfully");
            hash.Add("UM003", SO003);

            #endregion

            #region SubCategory

            FriendlyMessage SBO001 = new FriendlyMessage("SBO001", MessageType.Success, "Sub Category added successfully");
            hash.Add("SBO001", SBO001);

            FriendlyMessage SBO002 = new FriendlyMessage("SBO002", MessageType.Success, "Sub Category updated successfully");
            hash.Add("SBO002", SBO002);

            FriendlyMessage SBO003 = new FriendlyMessage("SBO003", MessageType.Success, "Sub Category deleted successfully");
            hash.Add("SBO003", SBO003);

            #endregion

            #region Dealers

            FriendlyMessage DO001 = new FriendlyMessage("DO001", MessageType.Success, "Dealer added successfully");
            hash.Add("DO001", DO001);

            FriendlyMessage DO002 = new FriendlyMessage("DO002", MessageType.Success, "Dealer updated successfully");
            hash.Add("DO002", DO002);

            FriendlyMessage DO003 = new FriendlyMessage("DO003", MessageType.Success, "Dealer deleted successfully");
            hash.Add("DO003", DO003);

            FriendlyMessage DO004 = new FriendlyMessage("DO004", MessageType.Success, "Dealer profile updated successfully");
            hash.Add("DO004", DO004);

            #endregion

            #region Product

            FriendlyMessage PO001 = new FriendlyMessage("PO001", MessageType.Success, "Product added successfully");
            hash.Add("PO001", PO001);

            FriendlyMessage PO002 = new FriendlyMessage("PO002", MessageType.Success, "Product updated successfully");
            hash.Add("PO002", PO002);

            FriendlyMessage PO003 = new FriendlyMessage("PO003", MessageType.Success, "Product deleted successfully");
            hash.Add("PO003", PO003);

            FriendlyMessage PO004 = new FriendlyMessage("PO004", MessageType.Success, "Error occur while uploading data.");
            hash.Add("PO004", PO004);

            FriendlyMessage PO005 = new FriendlyMessage("PO005", MessageType.Success, "Product excel uploaded successfully.");
            hash.Add("PO005", PO005);

            FriendlyMessage PO006 = new FriendlyMessage("PO006", MessageType.Success, "Products order placed successfully.");
            hash.Add("PO006", PO006);

            #endregion
			
            #region Vendors

            FriendlyMessage VO001 = new FriendlyMessage("VO001", MessageType.Success, "Vendor added successfully");
            hash.Add("VO001", VO001);

            FriendlyMessage VO002 = new FriendlyMessage("VO002", MessageType.Success, "Vendor updated successfully");
            hash.Add("VO002", VO002);

            FriendlyMessage VO003 = new FriendlyMessage("VO003", MessageType.Success, "Vendor deleted successfully");
            hash.Add("VO003", VO003);

            FriendlyMessage VO004 = new FriendlyMessage("VO004", MessageType.Success, "Vendor profile updated successfully");
            hash.Add("VO004", VO004);

            FriendlyMessage VO005 = new FriendlyMessage("VO005", MessageType.Success, "Vendor product mapped successfully");
            hash.Add("VO005", VO005);

            FriendlyMessage VO006 = new FriendlyMessage("VO006", MessageType.Success, "Vendor logo uploded successfully");
            hash.Add("VO006", VO006);
             
            #endregion
			
            #region Receivable

            FriendlyMessage RE001 = new FriendlyMessage("RE001", MessageType.Success, "Receivable added successfully");
            hash.Add("RE001", RE001);

            #endregion

            #region Purchase Order

            FriendlyMessage POR001 = new FriendlyMessage("POR001", MessageType.Success, "Purchase order added successfully");
            hash.Add("POR001", POR001);

            FriendlyMessage POR002 = new FriendlyMessage("POR002", MessageType.Success, "Purchase order updated successfully");
            hash.Add("POR002", POR002);

            FriendlyMessage POR003 = new FriendlyMessage("POR003", MessageType.Success, "Product item added successfully");
            hash.Add("POR003", POR003);

            FriendlyMessage POR004 = new FriendlyMessage("POR004", MessageType.Success, "Product item updated successfully");
            hash.Add("POR004", POR004);

            FriendlyMessage POR005 = new FriendlyMessage("POR005", MessageType.Success, "Product item deleted successfully");
            hash.Add("POR005", POR005);

            FriendlyMessage POR006 = new FriendlyMessage("POR006", MessageType.Success, "Email has been sent successfully.");
            hash.Add("POR006", POR006);

            #endregion

            #region Receivables

            FriendlyMessage RC001 = new FriendlyMessage("RC001", MessageType.Success, "Receivable data added successfully");
            hash.Add("RC001", RC001);

            FriendlyMessage RC002 = new FriendlyMessage("RC002", MessageType.Success, "Payment receipt send successfully");
            hash.Add("RC002", RC002);

            #endregion

            #region Payables

            FriendlyMessage PA001 = new FriendlyMessage("PA001", MessageType.Success, "Payable data added successfully");
            hash.Add("PA001", PA001);

            #endregion

            #region Tax

            FriendlyMessage TO001 = new FriendlyMessage("TO001", MessageType.Success, "Tax added successfully");
            hash.Add("TO001", TO001);

            FriendlyMessage TO002 = new FriendlyMessage("TO002", MessageType.Success, "Tax updated successfully");
            hash.Add("TO002", TO002);

            #endregion

            #region Invoice

            FriendlyMessage IO001 = new FriendlyMessage("IO001", MessageType.Success, "Email has been sent successfully.");
            hash.Add("IO001", IO001);

            #endregion
		}

		public static FriendlyMessage Get(string code)
		{
			FriendlyMessage message = hash.Find(code);

			return message;
		}

		/// <summary>
		/// struct to hold generic key and value
		/// </summary>
		/// <typeparam name="K">Key</typeparam>
		/// <typeparam name="V">Value</typeparam>
		/// <remarks></remarks>
		public struct KeyValue<K, V>
		{
			/// <summary>
			/// Gets or sets the key.
			/// </summary>
			/// <value>The key.</value>
			/// <remarks></remarks>
			public K Key
			{
				get;
				set;
			}
			/// <summary>
			/// Gets or sets the value.
			/// </summary>
			/// <value>The value.</value>
			/// <remarks></remarks>
			public V Value
			{
				get;
				set;
			}
		}

		/// <summary>
		/// FixedSizeGenericHashTable
		/// </summary>
		/// <typeparam name="K">Key</typeparam>
		/// <typeparam name="V">Value</typeparam>
		/// <remarks>Object for FixedSizeGenericHashTable of key K and of value V</remarks>
		public class FixedSizeGenericHashTable<K, V>
		{
			private readonly int size;
			private readonly LinkedList<KeyValue<K, V>>[] items;

			public FixedSizeGenericHashTable(int size)
			{
				this.size = size;
				items = new LinkedList<KeyValue<K, V>>[size];
			}

			protected int GetArrayPosition(K key)
			{
				int position = key.GetHashCode() % size;
				return Math.Abs(position);
			}

			public V Find(K key)
			{
				int position = GetArrayPosition(key);
				LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
				foreach(KeyValue<K, V> item in linkedList)
				{
					if(item.Key.Equals(key))
					{
						return item.Value;
					}
				}

				return default(V);
			}

			/// <summary>
			/// Adds the specified key.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <param name="value">The value.</param>
			/// <remarks></remarks>
			public void Add(K key, V value)
			{
				int position = GetArrayPosition(key);
				LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
				KeyValue<K, V> item = new KeyValue<K, V>()
				{
					Key = key,
					Value = value
				};
				linkedList.AddLast(item);
			}

			/// <summary>
			/// Removes the specified key.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <remarks></remarks>
			public void Remove(K key)
			{
				int position = GetArrayPosition(key);
				LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
				bool itemFound = false;
				KeyValue<K, V> foundItem = default(KeyValue<K, V>);
				foreach(KeyValue<K, V> item in linkedList)
				{
					if(item.Key.Equals(key))
					{
						itemFound = true;
						foundItem = item;
					}
				}

				if(itemFound)
				{
					linkedList.Remove(foundItem);
				}
			}

			/// <summary>
			/// Gets the linked list.
			/// </summary>
			/// <param name="position">The position.</param>
			/// <returns></returns>
			/// <remarks></remarks>
			protected LinkedList<KeyValue<K, V>> GetLinkedList(int position)
			{
				LinkedList<KeyValue<K, V>> linkedList = items[position];
				if(linkedList == null)
				{
					linkedList = new LinkedList<KeyValue<K, V>>();
					items[position] = linkedList;
				}

				return linkedList;
			}
		}
	}
}
