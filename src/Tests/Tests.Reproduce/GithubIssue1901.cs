﻿using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GithubIssue1901
	{
		private const string ProxyAuthResponse = @"<html>
<head><title>401 Authorization Required</title></head>
<body bgcolor=""white"">
<center><h1>401 Authorization Required</h1></center>
<hr><center>nginx/1.4.6 (Ubuntu)</center>
</body>
</html>
<!-- a padding to disable MSIE and Chrome friendly error page -->
<!-- a padding to disable MSIE and Chrome friendly error page -->
<!-- a padding to disable MSIE and Chrome friendly error page -->
<!-- a padding to disable MSIE and Chrome friendly error page -->
<!-- a padding to disable MSIE and Chrome friendly error page -->
<!-- a padding to disable MSIE and Chrome friendly error page -->";

		[U] public async Task BadAuthResponseDoesNotThrowExceptionWhenAttemptingToDeserializeResponse()
		{
			var client = FixedResponseClient.Create(ProxyAuthResponse, 401,
				s => s.SkipDeserializationForStatusCodes(401),
				"text/html",
				new Exception("problem with the request as a result of 401")
			);
			var source = await client.LowLevel.GetSourceAsync<GetResponse<Example>>("examples", "1");
			source.ApiCall.Success.Should().BeFalse();
		}

		[U] public async Task BadAuthCarriesStatusCodeAndResponseBodyOverToResponse()
		{
			var client = FixedResponseClient.Create(ProxyAuthResponse, 401,
				s => s.DisableDirectStreaming().SkipDeserializationForStatusCodes(401),
				"text/html",
				new Exception("problem with the request as a result of 401")
			);
			var response = await client.LowLevel.GetAsync<GetResponse<Example>>("examples", "1");
			response.ApiCall.Success.Should().BeFalse();
			response.ApiCall.ResponseBodyInBytes.Should().NotBeNullOrEmpty();
			response.ApiCall.HttpStatusCode.Should().Be(401);
		}

		private class Example { }
	}
}
