// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PublicApiFacts.cs" company="WildGums">
//   Copyright (c) 2008 - 2017 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.AutomaticSupport.Tests
{
    using ApiApprover;
    using NUnit.Framework;

    [TestFixture]
    public class PublicApiFacts
    {
        [Test]
        public void Orc_AutomaticSupport_HasNoBreakingChanges()
        {
            var assembly = typeof(AutomaticSupportService).Assembly;

            PublicApiApprover.ApprovePublicApi(assembly);
        }
    }
}