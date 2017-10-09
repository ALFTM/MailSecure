﻿using System;

namespace MailSecure.Cbc
{
	public abstract class Singleton<T> where T : new()
	{
		public static T instance = new T();

		public static T getInstance() {
			return instance;
		}
	}
}

