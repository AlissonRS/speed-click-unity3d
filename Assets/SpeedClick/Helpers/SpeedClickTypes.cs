using UnityEngine;
using System.Collections;

public enum ConnectionType {
	ServerConn,
	LocalConn
}

public enum Screens
{
	MainScreen,
	ScenesScreen,
	GameScreen,
	PauseScreen,
	LoginScreen,
	RegisterScreen,
    LoadingScreen,
    ScoreScreen}

public enum GameJoystickButtons
{
	Undefined = -1,
	Image1,
	Image2,
	Image3,
	Image4,
	Image5,
	Image6,
	Image7,
	Image8,
    Image9,
    Image10,
	MouseKey
}

public enum Commands
{
	Undefined,
	LoadScene,
	Login,
	ShowScreen,
	ExitGame,
	PauseGame,
	RegisterUser
}

public enum HttpMethodType
{
	Get,
	Post,
	Put,
	Delete
}

public enum Platforms: int
{
    WebGLPlayer,
    WebPlayer,
    IPhonePlayer,
    Android,
    WindowsPlayer,
    LinuxPlayer,
    OSXPlayer
}