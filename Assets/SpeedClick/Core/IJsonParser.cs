using System;
using Boomlagoon.JSON;

public interface IJSONParser 
{

	void ParseObject(JSONValue json);
    T To<T>();
}