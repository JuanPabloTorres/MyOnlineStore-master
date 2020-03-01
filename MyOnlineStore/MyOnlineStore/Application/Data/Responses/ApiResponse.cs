using MyOnlineStore.Application.Common.Interfaces.Failures;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Application.Data.Responses
{
    public class ApiResponse
    {
		private bool isFailure;

        public bool IsFailure
		{
			get { return isFailure; }
			set { isFailure = value; }
		}

        public Either<IFailure, dynamic> Response { get; set; }

	}

    public class Failure : IFailure
    {
        private string message;
		public string Message
		{
			get { return message; }
			set { message = value; }
		}
	}

	public class Either<Left, Right> where Left : class where Right : class
	{
		private Left leftside;
		private Right rightside;

        public Left LeftSide
        {
            get { return leftside; }
            protected set { leftside = value; }
        }

		public Right RightSide
		{
			get { return rightside; }
			protected set { rightside = value; }
		}
	}
}
