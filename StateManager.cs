using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Globalization;
using System.IO;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.VisualBasic;
using System.Windows;
using System.Xml;
using System.Web;
//using System.Web.Caching;

namespace FinalProject
{
	public class StateManager
	{
		// Cache object that will be used to store and retrieve items from
		// the cache and constants used within this object
		// private static Cache myCache = System.Web.HttpRuntime.Cache();
		private static string stateKey = "StateKey";
		public static string applicationConstantsFileName = AppDomain.CurrentDomain.BaseDirectory + "State.xml"; // = Strings.Replace(System.AppDomain.CurrentDomain.BaseDirectory + "State.xml", "/", @"\");
																												 // "States.config", "/", "\")
		private static state[] stateArray;
		private static ArrayList errorList;


		// Tells you whether or not any errors have occurred w/in the module
		public static bool hasErrors
		{
			get
			{
				if (errorList == null || errorList.Count == 0)
					return false;
				else
					return true;
			}
		}


		// Retrieves an array list of Exception objects
		public static ArrayList getErrors
		{
			get
			{
				return errorList;
			}
		}


		// Private method used to add errors to the errorList
		private static void addError(ref Exception e)
		{
			if (errorList == null)
				errorList = new ArrayList();
			errorList.Add(e);
		}

		/// <summary>
		///     ''' Gets all the states
		///     ''' </summary>
		///     ''' <returns>An array of State objects</returns>
		public static state[] getStates()
		{
			//if (myCache[stateKey] == null)
			PopulateCache();
			return stateArray;
		}


		/// <summary>
		///     ''' Takes the abbreviation given and returns the full name
		///     ''' </summary>
		///     ''' <returns>The full name for the abbreviation in 
		///     ''' question</returns>
		private static string convertAbbreviationToName(ref string abbreviation)
		{
			XmlDocument xmlFile = new XmlDocument();

			try
			{
				applicationConstantsFileName = applicationConstantsFileName.Replace("/", @"\");
				xmlFile.Load(applicationConstantsFileName);
				XmlNode theNode = xmlFile.SelectSingleNode("descendant::state[@abbreviation='" + abbreviation + "']");

				if (theNode != null)
					return theNode.Attributes.GetNamedItem("name").Value;
			}
			catch (Exception e)
			{
				addError(ref e);
			}

			return null;
		}


		/// <summary>
		///     ''' Gets the state object based on the full name
		///     ''' </summary>
		///     ''' <param name="name">The full name of the state to 
		///     ''' retrieve</param>
		///     ''' <returns>A State object for the name given</returns>
		public static state getStateByName(ref string name)
		{
			// if (myCache[stateKey + name] == null)
			PopulateCache();
			//return state[stateKey + name];

			return (state)Convert.ChangeType(stateKey, typeof(state[]));
		}


		/// <summary>
		///     ''' Gets the state object based on the abbreviation
		///     ''' </summary>
		///     ''' <param name="abbreviation">The abbreviation of the state 
		///     ''' to retrieve</param>
		///     ''' <returns>A State object for the abbreviation 
		///     ''' given</returns>
		public static state getStateByAbbreviation(ref string abbreviation)
		{
			string name = convertAbbreviationToName(ref abbreviation);
			if (name != null)
				return getStateByName(ref name);
			else
				return null/* TODO Change to default(_) if this is not a reference type */;
		}


		/// <summary>The manager attempts to load the XML
		///     ''' file and store it in the cache with a dependency on the XML 
		///     ''' file itself.' This means that any time the XML file changes, it 
		///     ''' is removed from the cache.  When the methods that return State 
		///     ''' objects are called again, the XML file won't exist in memory 
		///     ''' and the PopulateCache will be re-called.
		///     ''' </summary>
		private static void PopulateCache()
		{
			XmlDocument xmlFile = new XmlDocument();
			// Dim theState As State
			XmlNode theNode;
			string theName, theAbbreviation;
			int i = 0;

			try
			{
				applicationConstantsFileName = applicationConstantsFileName.Replace("/", @"\");
				xmlFile.Load(applicationConstantsFileName);

				// Attempt to find the element given the "key" for that tag
				XmlNodeList elementList = xmlFile.GetElementsByTagName("state");

				if (elementList != null)
				{
					stateArray = (state[])Array.CreateInstance(typeof(state), elementList.Count);

					// Loop through each element that has the name we're looking for
					for (i = 0; i <= elementList.Count - 1; i++)
					{
						theNode = elementList[i];

						// Get the name for that tag
						if (theNode.Attributes.GetNamedItem("name") != null)
							theName = theNode.Attributes.GetNamedItem("name").Value;
						else
							theName = null;

						// Get the abbreviation for that tag
						if (theNode.Attributes.GetNamedItem("abbreviation") != null)
							theAbbreviation = theNode.Attributes.GetNamedItem("abbreviation").Value;
						else
							theAbbreviation = null;

						// Populate that location in the array with the
						// values for the tag
						stateArray[i] = new state(ref theName, ref theAbbreviation);

						// Insert the state into cache
						//// myCache.Insert(stateKey + theName, stateArray[i], new CacheDependency(applicationConstantsFileName));
					}

					// Insert the state array into cache
					////  myCache.Insert(stateKey, stateArray, new CacheDependency(applicationConstantsFileName));
				}
			}
			catch (Exception e)
			{
				addError(ref e);
			}
		}
	}
}