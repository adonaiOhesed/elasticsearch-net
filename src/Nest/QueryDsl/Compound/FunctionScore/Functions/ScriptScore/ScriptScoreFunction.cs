﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// The script score function allows you to wrap another query and customize the
	/// scoring of it optionally with a computation derived from other numeric
	/// field values in the doc using a script expression.
	/// </summary>
	[InterfaceDataContract]
	public interface IScriptScoreFunction : IScoreFunction
	{
		/// <summary>
		/// The script to execute to calculate score
		/// </summary>
		[DataMember(Name = "script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc cref="IScriptScoreFunction"/>
	public class ScriptScoreFunction : FunctionScoreFunctionBase, IScriptScoreFunction
	{
		/// <inheritdoc />
		public IScript Script { get; set; }
	}

	/// <inheritdoc cref="IScriptScoreFunction"/>
	public class ScriptScoreFunctionDescriptor<T>
		: FunctionScoreFunctionDescriptorBase<ScriptScoreFunctionDescriptor<T>, IScriptScoreFunction, T>, IScriptScoreFunction
		where T : class
	{
		IScript IScriptScoreFunction.Script { get; set; }

    /// <inheritdoc cref="IScriptScoreFunction.Script"/>
	public ScriptScoreFunctionDescriptor<T> Script(Func<ScriptDescriptor, IScript> selector) =>
		Assign(selector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
