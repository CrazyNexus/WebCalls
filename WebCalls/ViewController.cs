﻿//
// ViewController.cs
//
// Created by Thomas Dubiel on 22.12.2016
// Copyright 2016 Thomas Dubiel. All rights reserved.
//
using System;

using UIKit;
using System.Net;
using Foundation;
using System.IO;
using RestSharp;

namespace WebCalls
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			var client = new RestClient("http://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/");

			var request = new RestRequest(String.Format("{0}/allinfo", "198440"));
			//client.ExecuteAsync(request, response =>
			//{
			//	Console.WriteLine(response.Content);


			//});

			var response = client.Execute<Item>(request);
			Console.WriteLine(response.Data.rxtermsProperties);

		}

		void RunHttpRequest()
		{
			var request = HttpWebRequest.Create(@"http://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/198440/allinfo");
			request.ContentType = "application/json";
			request.Method = "GET";

			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if (response.StatusCode != HttpStatusCode.OK)
					Console.WriteLine("Status Code not OK");

				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					var content = reader.ReadToEnd();
					if (string.IsNullOrWhiteSpace(content))
					{
						Console.WriteLine("Response String not valid");
					}
					else {
						Console.WriteLine("Response: " + content);
					}
				}
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}

	public class Item
	{
		public string rxtermsProperties { get; set; }
	}
}
