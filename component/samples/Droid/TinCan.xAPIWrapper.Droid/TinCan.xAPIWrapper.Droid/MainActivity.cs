#region License and Warranty Information

// ==========================================================
//  <copyright file="MainActivity.cs" company="iWork Technologies">
//  Copyright (c) 2015 All Right Reserved, http://www.iworktech.com/
// 
//  This source is subject to the iWork Technologies Permissive License.
//  Please see the License.txt file for more information.
//  All other rights reserved. 
// 
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// 
//  </copyright>
//  <author>Ameet Shedge</author>
//  <email>info@iworktech.com</email>
//  <date>2017-01-05</date>
// ===========================================================

#endregion

#region

using Android.App;
using Android.Widget;
using Android.OS;

#endregion

namespace TinCan.xAPIWrapper.Droid
{
    [Activity(Label = "TinCan.xAPIWrapper.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int _count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += async delegate {
                var apiWrapper = new APIWrapper(string.Empty, string.Empty, string.Empty);
                var statement = apiWrapper.PrepareStatement("test@ald.net", "experienced", "Activity");
                var task = await apiWrapper.SendStatement(statement);

                if (task.Success)
                {
                    button.Text = $"{_count++} sent!";
                }
            };
        }
    }
}