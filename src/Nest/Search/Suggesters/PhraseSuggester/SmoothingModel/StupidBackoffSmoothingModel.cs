﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(StupidBackoffSmoothingModel))]
	public interface IStupidBackoffSmoothingModel : ISmoothingModel
	{
		[DataMember(Name = "discount")]
		double? Discount { get; set; }
	}

	public class StupidBackoffSmoothingModel : SmoothingModelBase, IStupidBackoffSmoothingModel
	{
		public double? Discount { get; set; }

		internal override void WrapInContainer(ISmoothingModelContainer container) => container.StupidBackoff = this;
	}

	public class StupidBackoffSmoothingModelDescriptor
		: DescriptorBase<StupidBackoffSmoothingModelDescriptor, IStupidBackoffSmoothingModel>, IStupidBackoffSmoothingModel
	{
		double? IStupidBackoffSmoothingModel.Discount { get; set; }

		public StupidBackoffSmoothingModelDescriptor Discount(double? discount) => Assign(discount, (a, v) => a.Discount = v);
	}
}
