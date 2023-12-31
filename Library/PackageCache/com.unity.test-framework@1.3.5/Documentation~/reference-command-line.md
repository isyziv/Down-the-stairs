# Running tests from the command line

It’s pretty simple to run a test project from the command line. Here is an example in Windows:

```bash
Unity.exe -runTests -batchmode -projectPath PATH_TO_YOUR_PROJECT -testResults C:\temp\results.xml -testPlatform PS4
```

> **Note**: Use the `-batchmode` option when running tests on the command line to remove the need for manual user inputs. For more information, see Unity [Command line arguments](https://docs.unity3d.com/Manual/CommandLineArguments.html).

## Test Framework command line arguments

### forgetProjectPath

Don't save your current **Project** into the Unity launcher/hub history.

### runTests

Runs tests in the Project.

### testCategory

A semicolon-separated list of test categories to include in the run, or a regular expression pattern to match category names. A semi-colon separated list should be formatted as a string enclosed in quotation marks, e.g. `testCategory "firstCategory;secondCategory"`. If using both `testFilter` and `testCategory`, then only tests that match both are run. This argument supports negation using '!'. If using '!MyCategory' then no tests with the 'MyCategory' category will be included in the run.

### testFilter

A semicolon-separated list of test names to run, or a regular expression pattern to match tests by their full name. A semi-colon separated list should be formatted as a string enclosed in quotation marks, e.g. `testFilter "Low;Medium"`. This argument supports negation using '!'. If using the test filter '!MyNamespace.Something.MyTest', then all tests except that test will be run.
It is also possible to run a specific variation of a parameterized test like so: `"ClassName\.MethodName\(Param1,Param2\)"`

### testPlatform

The platform to run tests on. Accepted values: 

* **EditMode**
    * Edit Mode tests. Equivalent to running tests from the EditMode tab of the Test Runner window.
* **PlayMode**
    * Play Mode tests that run in the Editor. Equivalent to running tests from the PlayMode tab of the Test Runner window.
* Any value from the [BuildTarget](https://docs.unity3d.com/ScriptReference/BuildTarget.html) enum.
    * Play Mode tests that run on a player built for the specified platform. Equivalent to using the **Run all tests (`<target_platform>`)** dropdown in the PlayMode tab of the Test Runner window.

> **Note**: If no value is specified for this argument, tests run in Edit Mode.

### assemblyNames

A semicolon-separated list of test assemblies to include in the run. A semi-colon separated list should be formatted as a string enclosed in quotation marks, e.g. `assemblyNames "firstAssembly;secondAssembly"`.

### testResults

The path where Unity should save the result file. By default, Unity saves it in the Project’s root folder. Test results follow the XML format as defined by NUnit, see the [NUnit documentation](https://docs.nunit.org/articles/nunit/technical-notes/usage/Test-Result-XML-Format.html). There is currently no common definition for exit codes reported by individual Unity components under test. The best way to understand the source of a problem is the content of error messages and stack traces.

### playerHeartbeatTimeout

The time, in seconds, the editor should wait for heartbeats after starting a test run on a player. This defaults to 10 minutes.

### runSynchronously

If included, the test run will run tests synchronously, guaranteeing that all tests runs in one editor update call. Note that this is only supported for EditMode tests, and that tests which take multiple frames (i.e. `[UnityTest]` tests, or tests with `[UnitySetUp]` or `[UnityTearDown]` scaffolding) will be filtered out.

### testSettingsFile 

Path to a *TestSettings.json* file that allows you to set up extra options for your test run. An example of the *TestSettings.json* file could look like this:

```json
{
  "scriptingBackend":"WinRTDotNET",
  "Architecture":null,
  "apiProfile":0,
  "featureFlags": { "requiresSplashScreen": true },
  "webGLClientBrowserType": "Safari",
  "webGLClientBrowserPath": "/Applications/Safari.app"
}
```

#### apiProfile

The .Net compatibility level. Set to one of the following values:  

- 1 - .Net 2.0 
- 2 - .Net 2.0 Subset 
- 3 - .Net 4.6 
- 5 - .Net micro profile (used by Mono scripting backend if **Stripping Level** is set to **Use micro mscorlib**) 
- 6 - .Net Standard 2.0 

#### appleEnableAutomaticSigning

Sets option for automatic signing of Apple devices.

#### appleDeveloperTeamID 

Sets the team ID for the apple developer account.

#### architecture

Target architecture for Android. Set to one of the following values: 

* None = 0
* ARMv7 = 1
* ARM64 = 2
* X86 = 4
* All = 4294967295

#### iOSManualProvisioningProfileType

Set to one of the following values: 

* 0 - Automatic 
* 1 - Development 
* 2 - Distribution iOSManualProvisioningProfileID

#### iOSTargetSDK

Target SDK for iOS. Set to one of the following values, which should be given as a string literal enclosed in quotes:

* DeviceSDK
* SimulatorSDK

#### tvOSTargetSDK

Target SDK for tvOS. Set to one of the following values, which should be given as a string literal enclosed in quotes:

* DeviceSDK
* SimulatorSDK

#### scriptingBackend

 Set to one of the following values, which should be given as a string literal enclosed in quotes:

- Mono2x
- IL2CPP
- WinRTDotNET

#### playerGraphicsAPI

 Set graphics API that will be used during test execution in the player. Value can be any [GraphicsDeviceType](https://docs.unity3d.com/ScriptReference/Rendering.GraphicsDeviceType.html) as a string literal enclosed in quotes. Value will only be set if it is supported on the target platform.

 #### webGLClientBrowserType

A browser to be used when running test using WebGL platform. Accepted browser types:

- Safari
- Firefox
- Chrome
- Chromium

 #### webGLClientBrowserPath

 An absolute path to the browser's location on your device. If not defined, path from UNITY_AUTOMATION_DEFAULT_BROWSER_PATH enviromental variable will be used.

### androidBuildAppBundle

A boolean setting that allows to build an Android App Bundle (AAB) instead of APK for tests.

### orderedTestListFile
Path to a *.txt* file which contains a list of full test names you want to run in the specified order. The tests should be seperated by new lines and if they have parameters, these should be specified as well. A list of the file could look like this:
```
Unity.Framework.Tests.OrderedTests.NoParameters
Unity.Framework.Tests.OrderedTests.ParametrizedTestA(3,2)
Unity.Framework.Tests.OrderedTests.ParametrizedTestB("Assets/file.fbx")
Unity.Framework.Tests.OrderedTests.ParametrizedTestC(System.String[],"foo.fbx")
Unity.Framework.Tests.OrderedTests.ParametrizedTestD(1.0f)
Unity.Framework.Tests.OrderedTests.ParametrizedTestE(null)
Unity.Framework.Tests.OrderedTests.ParametrizedTestF(False, 1)
Unity.Framework.Tests.OrderedTests.ParametrizedTestG(float.NaN)
Unity.Framework.Tests.OrderedTests.ParametrizedTestH(SomeEnum)
```
### retry
An integer that set the retry count. The test that fail will be retried the amount of times set using this or until the test succeeds.

### repeat
An integer that set the repeat count. The tests will be repeated the amount of times set using this or until the test fails.

### featureFlags
Map of strings and boolean values which can switch Unity Test Framework features on or off. The currently supported features are:

* fileCleanUpCheck
Throws an error message (instead of warning) if tests generate files which are not cleaned up. False (off) by default.

* requiresSplashScreen
By default UTR disables the Made with Unity splash screen to speed up building the player and running tests. Set this flag to `true` to override the default and always require a splash screen to be built.
