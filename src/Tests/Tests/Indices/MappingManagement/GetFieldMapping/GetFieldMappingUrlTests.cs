﻿using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Nest.Infer;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.MappingManagement.GetFieldMapping
{
	public class GetFieldMappingUrlTests
	{
		[U]
		public async Task Urls()
		{
			var index = "index1,index2";
			Nest.Indices indices = index;
			var fields = Field<Project>(p => p.Name).And("field");
			await GET($"/_mapping/field/name%2Cfield")
					.Request(c => c.GetFieldMapping(new GetFieldMappingRequest(fields)))
					.RequestAsync(c => c.GetFieldMappingAsync(new GetFieldMappingRequest(fields)))
				;

			await GET($"/index1%2Cindex2/_mapping/field/name%2Cfield")
					.Fluent(c => c.GetFieldMapping<Project>(fields, g => g.Index(index)))
					.Request(c => c.GetFieldMapping(new GetFieldMappingRequest(indices, fields)))
					.FluentAsync(c => c.GetFieldMappingAsync<Project>(fields, g => g.Index(index)))
					.RequestAsync(c => c.GetFieldMappingAsync(new GetFieldMappingRequest(indices, fields)))
				;

		}
	}
}
