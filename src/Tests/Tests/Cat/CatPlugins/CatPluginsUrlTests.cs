﻿using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatPlugins
{
	public class CatPluginsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/plugins")
			.Fluent(c => c.CatPlugins())
			.Request(c => c.CatPlugins(new CatPluginsRequest()))
			.FluentAsync(c => c.CatPluginsAsync())
			.RequestAsync(c => c.CatPluginsAsync(new CatPluginsRequest()));
	}
}
