﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// The GeoIP processor adds information about the geographical location of IP addresses,
	/// based on data from the Maxmind databases.
	/// This processor adds this information by default under the geoip field.
	/// The geoip processor can resolve both IPv4 and IPv6 addresses.
	/// </summary>
	/// <remarks>
	/// Requires the Ingest Geoip Processor Plugin to be installed on the cluster.
	/// </remarks>
	[InterfaceDataContract]
	public interface IGeoIpProcessor : IProcessor
	{
		[DataMember(Name ="database_file")]
		string DatabaseFile { get; set; }

		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// If `true` and `field` does not exist, the processor quietly exits without modifying the document
		/// </summary>
		[DataMember(Name ="ignore_missing")]
		bool? IgnoreMissing { get; set; }

		[DataMember(Name ="properties")]
		IEnumerable<string> Properties { get; set; }

		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }
	}

	/// <summary>
	/// The GeoIP processor adds information about the geographical location of IP addresses,
	/// based on data from the Maxmind databases.
	/// This processor adds this information by default under the geoip field.
	/// The geoip processor can resolve both IPv4 and IPv6 addresses.
	/// </summary>
	/// <remarks>
	/// Requires the Ingest Geoip Processor Plugin to be installed on the cluster.
	/// </remarks>
	public class GeoIpProcessor : ProcessorBase, IGeoIpProcessor
	{
		public string DatabaseFile { get; set; }

		public Field Field { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		public IEnumerable<string> Properties { get; set; }

		public Field TargetField { get; set; }
		protected override string Name => "geoip";
	}

	/// <summary>
	/// The GeoIP processor adds information about the geographical location of IP addresses,
	/// based on data from the Maxmind databases.
	/// This processor adds this information by default under the geoip field.
	/// The geoip processor can resolve both IPv4 and IPv6 addresses.
	/// </summary>
	/// <remarks>
	/// Requires the Ingest Geoip Processor Plugin to be installed on the cluster.
	/// </remarks>
	public class GeoIpProcessorDescriptor<T>
		: ProcessorDescriptorBase<GeoIpProcessorDescriptor<T>, IGeoIpProcessor>, IGeoIpProcessor
		where T : class
	{
		protected override string Name => "geoip";
		string IGeoIpProcessor.DatabaseFile { get; set; }

		Field IGeoIpProcessor.Field { get; set; }
		bool? IGeoIpProcessor.IgnoreMissing { get; set; }
		IEnumerable<string> IGeoIpProcessor.Properties { get; set; }
		Field IGeoIpProcessor.TargetField { get; set; }

		public GeoIpProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public GeoIpProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc />
		public GeoIpProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);

		public GeoIpProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		public GeoIpProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		public GeoIpProcessorDescriptor<T> DatabaseFile(string file) => Assign(file, (a, v) => a.DatabaseFile = v);

		public GeoIpProcessorDescriptor<T> Properties(IEnumerable<string> properties) => Assign(properties, (a, v) => a.Properties = v);

		public GeoIpProcessorDescriptor<T> Properties(params string[] properties) => Assign(properties, (a, v) => a.Properties = v);
	}
}
