#region License and Warranty Information

// ==========================================================
//  <copyright file="RemoteLRSResourceTest.cs" company="iWork Technologies">
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
//  <author>iWorkTech Dev</author>
//  <email>info@iworktech.com</email>
//  <date>2017-01-05</date>
// ===========================================================

#endregion

#region

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TinCan.Standard;
using TinCan.Standard.Documents;

#endregion

namespace xAPIWrapper.Tests.Droid
{
    //// <summary>
    /// Class RemoteLRSResourceTest.
    /// </summary>
    [TestFixture]
    public class RemoteLRSResourceTest
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            Console.WriteLine("Running " + TestContext.CurrentContext.Test.FullName);

            //
            // these are credentials used by the other OSS libs when building via Travis-CI
            // so are okay to include in the repository, if you wish to have access to the
            // results of the test suite then supply your own endpoint, username, and password
            //
            _lrs = new RemoteLRS(
                "https://lrs.adlnet.gov/xAPI/",
                "Nja986GYE1_XrWMmFUE",
                "Bd9lDr1kjaWWY6RID_4"
            );
        }

        /// <summary>
        /// The LRS
        /// </summary>
        RemoteLRS _lrs;

        /// <summary>
        /// Tests the about.
        /// </summary>
        [Test]
        public async Task TestAboutAsync()
        {
            var lrsRes = await _lrs.AboutAsync();
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the about failure.
        /// </summary>
        [Test]
        public void TestAboutFailure()
        {
            //_lrs.Endpoint = new Uri("http://cloud.scorm.com/tc/3TQLAI9/sandbox/");
            _lrs.Endpoint = new Uri("https://lrs.adlnet.gov/");

            var lrsRes = _lrs.About();
            Assert.IsFalse(lrsRes.Success);
            Console.WriteLine("TestAboutFailure - errMsg: " + lrsRes.ErrMsg);
        }

        /// <summary>
        /// Tests the state of the clear.
        /// </summary>
        [Test]
        public async Task TestClearStateAsync()
        {
            var lrsRes = await _lrs.ClearStateAsync(Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the delete activity profile.
        /// </summary>
        [Test]
        public async Task TestDeleteActivityProfileAsync()
        {
            var doc = new ActivityProfileDocument
            {
                Activity = Support.Activity,
                ID = Guid.NewGuid().ToString()
            };

            var lrsRes = await _lrs.DeleteActivityProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the delete agent profile.
        /// </summary>
        [Test]
        public async Task TestDeleteAgentProfileAsync()
        {
            var doc = new AgentProfileDocument
            {
                Agent = Support.Agent,
                ID = Guid.NewGuid().ToString()
            };

            var lrsRes = await _lrs.DeleteAgentProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the state of the delete.
        /// </summary>
        [Test]
        public async Task TestDeleteStateAsync()
        {
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                ID = Guid.NewGuid().ToString()
            };

            var lrsRes = await _lrs.DeleteStateAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the more statements.
        /// </summary>
        [Test]
        public async Task TestMoreStatementsAsync()
        {
            var query = new StatementsQuery
            {
                Format = StatementsQueryResultFormat.IDS,
                Limit = 2
            };

            var queryRes = await _lrs.QueryStatementsAsync(query);
            if (queryRes.Success && queryRes.Content.More != null)
            {
                var moreRes = _lrs.MoreStatements(queryRes.Content);
                Assert.IsTrue(queryRes.Success);
                Assert.IsNotNull(moreRes);
                Console.WriteLine("TestMoreStatementsAsync - statement count: " + queryRes.Content.Statements.Count);
            }
        }

        /// <summary>
        /// Tests the query statements.
        /// </summary>
        [Test]
        public async Task TestQueryStatementsAsync()
        {
            var query = new StatementsQuery
            {
                Agent = Support.Agent,
                VerbId = Support.Verb.ID,
                ActivityId = Support.Parent.ID,
                RelatedActivities = true,
                RelatedAgents = true,
                Format = StatementsQueryResultFormat.IDS,
                Limit = 2
            };

            var lrsRes = await _lrs.QueryStatementsAsync(query);
            Assert.IsTrue(lrsRes.Success);
            Console.WriteLine("TestQueryStatements - statement count: " + lrsRes.Content.Statements.Count);
        }

        /// <summary>
        /// Tests the retrieve activity profile.
        /// </summary>
        [Test]
        public async Task TestRetrieveActivityProfileAsync()
        {
            var lrsRes = await _lrs.RetrieveActivityProfileAsync("test", Support.Activity);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<ActivityProfileDocument>(lrsRes.Content);
        }

        /// <summary>
        /// Tests the retrieve activity profile ids.
        /// </summary>
        [Test]
        public async Task TestRetrieveActivityProfileIdsAsync()
        {
            var lrsRes = await _lrs.RetrieveActivityProfileIdsAsync(Support.Activity);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the retrieve agent profile.
        /// </summary>
        [Test]
        public async Task TestRetrieveAgentProfileAsync()
        {
            var lrsRes = await _lrs.RetrieveAgentProfileAsync("test", Support.Agent);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<AgentProfileDocument>(lrsRes.Content);
        }

        /// <summary>
        /// Tests the retrieve agent profile ids.
        /// </summary>
        [Test]
        public async Task TestRetrieveAgentProfileIdsAsync()
        {
            var lrsRes = await _lrs.RetrieveAgentProfileIdsAsync(Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }
    

        /// <summary>
        /// Tests the state of the retrieve.
        /// </summary>
        [Test]
        public async Task TestRetrieveStateAsync()
        {
            var lrsRes = await _lrs.RetrieveStateAsync("test", Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<StateDocument>(lrsRes.Content);
        }

        
        /// <summary>
        /// Tests the retrieve state ids.
        /// </summary>
        [Test]
        public async Task TestRetrieveStateIdsAsync()
        {
            var lrsRes = await _lrs.RetrieveStateIdsAsync(Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

       
        /// <summary>
        /// Tests the retrieve statement.
        /// </summary>
        [Test]
        public async Task TestRetrieveStatementAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.Activity;
            statement.Context = Support.Context;
            statement.Result = Support.Result;

            var saveRes = _lrs.SaveStatement(statement);
            if (!saveRes.Success) return;
            if (saveRes.Content.ID == null) return;
            var retRes = await _lrs.RetrieveStatementAsync(saveRes.Content.ID.Value);
            Assert.IsTrue(retRes.Success);
            Console.WriteLine("TestRetrieveStatement - statement: " + retRes.Content.ToJSON(true));
        }

     

        /// <summary>
        /// Tests the save activity profile.
        /// </summary>
        [Test]
        public async Task TestSaveActivityProfileAsync()
        {
            var doc = new ActivityProfileDocument
            {
                Activity = Support.Activity,
                ID = Guid.NewGuid().ToString(),
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await _lrs.SaveActivityProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }


        /// <summary>
        /// Tests the save agent profile.
        /// </summary>
        [Test]
        public async Task TestSaveAgentProfileAsync()
        {
            var doc = new AgentProfileDocument
            {
                Agent = Support.Agent,
                ID = Guid.NewGuid().ToString(),
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await _lrs.SaveAgentProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        /// <summary>
        /// Tests the state of the save.
        /// </summary>
        [Test]
        public async Task TestSaveStateAsync()
        {
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                ID = Guid.NewGuid().ToString(),
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await _lrs.SaveStateAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        

        /// <summary>
        /// Tests the save statement.
        /// </summary>
        [Test]
        public async Task TestSaveStatementAsync()
        {
            var statement = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity
            };

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
            Assert.IsNotNull(lrsRes.Content.ID);
        }

        /// <summary>
        /// Tests the save statements.
        /// </summary>
        [Test]
        public async Task TestSaveStatementsAsync()
        {
            var statement1 = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Parent
            };

            var statement2 = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity,
                Context = Support.Context
            };

            var statements = new List<Statement> { statement1, statement2 };

            var lrsRes = await _lrs.SaveStatementsAsync(statements);
            Assert.IsTrue(lrsRes.Success);
            // TODO: check statements match and ids not null
        }

        /// <summary>
        /// Tests the save statement statement reference.
        /// </summary>
        [Test]
        public async Task TestSaveStatementStatementRefAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.StatementRef;

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        /// <summary>
        /// Tests the save statement sub statement.
        /// </summary>
        [Test]
        public async Task TestSaveStatementSubStatementAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.SubStatement;

            Console.WriteLine(statement.ToJSON(true));

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        /// <summary>
        /// Tests the save statement with identifier.
        /// </summary>
        [Test]
        public async Task TestSaveStatementWithIDAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.Activity;

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        /// <summary>
        /// Tests the void statement.
        /// </summary>
        [Test]
        public async Task TestVoidStatementAsync()
        {
            var toVoid = Guid.NewGuid();
            var lrsRes = await _lrs.VoidStatementAsync(toVoid, Support.Agent);

            Assert.IsTrue(lrsRes.Success, "LRS response successful");
            Assert.AreEqual(new Uri("http://adlnet.gov/expapi/verbs/voided"), lrsRes.Content.Verb.ID,
                "voiding statement uses voided verb");
            Assert.AreEqual(toVoid, ((StatementRef)lrsRes.Content.Target).ID, "voiding statement target correct id");
        }
    }
}