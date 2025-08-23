# 🧪 Web UI Automation Framework (C# + Selenium)

> 🚧 **This framework is currently under development.** New tests and features are being added progressively, including architecture improvements.

This is a UI automation testing framework built using **C#**, **Selenium WebDriver**, and **NUnit**. It follows the **Page Object Model (POM)** design pattern to ensure clean, scalable, 
and maintainable test code. The current test target is [saucedemo.com](https://www.saucedemo.com).

---

## 🚀 Features

- ✅ UI automation with **Selenium WebDriver**
- 🧱 Page Object Model (POM) architecture
- 🧪 Test execution via **NUnit**
- ⚙️ Environment config via `.env` file
- 📝 **Logging with Serilog**
- 📊 **HTML reporting with ExtentReports**
- 🔁 Ready for data-driven tests (CSV/JSON)
- 🔧CI/CD GitHub Actions

---


---

## 🧑‍💻 Technologies

- **Language:** C#
- **Test Framework:** NUnit
- **Automation:** Selenium WebDriver
- **Logging:** Serilog
- **Reporting:** ExtentReports
- **Config:** DotNetEnv
- **Data Handling:** Newtonsoft.Json

---

## ✅ Example Test

```c#
        [Test]
        public void Login_WithLockedUser_ShouldDenyAccess()
        {
            string username = EnvConfig.LockedUser;
            string password = EnvConfig.LockedUserPassword;
            string expectedError = "Epic sadface: Sorry, this user has been locked out.";
            Uri expectedUri = new Uri(ConfigManager.Settings.baseUrlSaucedemo);

            var loginPage = new LoginPage(driver);
            loginPage.GoToUrl(ConfigManager.Settings.baseUrlSaucedemo);
            Assert.IsTrue(loginPage.IsLoaded(), "Login Page did not load correctly");

            loginPage.LoginAsInvalidUser(username, password);

            Uri actualUri = new Uri(loginPage.GetUrl());

            Assert.Multiple(() => 
            {
                Assert.That(actualUri, Is.EqualTo(expectedUri));
                StringAssert.AreEqualIgnoringCase(expectedError, loginPage.GetErrorText());            
            });
        }

```

## 📄 Logs & Reports
* 📝 Logs are generated using Serilog for each test session.
* 📊 Detailed HTML reports are created using ExtentReports and saved under the Reports/ folder.

## 🛣️ Roadmap
* [x] Login tests (valid, locked, invalid credentials)
* [x] Serilog integration for logging
* [x] ExtentReports setup
* [x] Add GitHub Actions for CI
* [ ] Product/cart tests
* [ ] Add support for parallel execution

## 📜 License
MIT License — Free to use for educational and professional purposes.

## 👤 Author
Max Cortes Serrano
QA Engineer
[LinkedIn Profile](https://www.linkedin.com/in/max-cortés-6a132b21b)
Email: maxcortes23@gmail.com
