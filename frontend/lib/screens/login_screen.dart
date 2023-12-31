import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:frontend/common/common.dart';
import 'package:frontend/domain/models/user_credentials.dart';
import 'package:frontend/main.dart';
import 'package:frontend/screens/register_screen.dart';
import 'package:frontend/services/user_service.dart';
import 'package:frontend/widgets/login_with_facebook_button.dart';
import 'package:frontend/widgets/login_with_google_button.dart';
import 'package:provider/provider.dart';

import '../domain/models/auth_result.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<StatefulWidget> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  bool _isEmailValid(String email) =>
      RegExp(r"^[a-zA-Z0-9.]+@[a-zA-Z0-9]+\.[a-zA-Z]+").hasMatch(email);
  final _formKey = GlobalKey<FormState>();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  final _storage = const FlutterSecureStorage();
  Locale _locale = Locale('en');

  void setLocale(Locale value) {
    setState(() {
      _locale = value;
    });
  }

  bool _isFormValid = false;

  bool _areAllFieldsValid() {
    if (_emailController.text.isEmpty ||
        !_isEmailValid(_emailController.text)) {
      setState(() {
        _isFormValid = false;
      });
      return false;
    }
    if (_passwordController.text.isEmpty ||
        _passwordController.text.length < 5) {
      setState(() {
        _isFormValid = false;
      });
      return false;
    }
    setState(() {
      _isFormValid = true;
    });
    return true;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      resizeToAvoidBottomInset: false,
      body: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Padding(
              padding: const EdgeInsets.only(left: 10),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  const Row(
                    children: [
                      Text(
                        "Don't have an ",
                        style: TextStyle(
                            fontFamily: "PTSansNarrow",
                            fontSize: 30,
                            color: Color.fromARGB(255, 43, 43, 43)),
                      ),
                      Text(
                        "account?",
                        style: TextStyle(
                          fontFamily: "PTSansNarrow",
                          fontSize: 30,
                          color: Color.fromARGB(255, 39, 33, 234),
                        ),
                      ),
                    ],
                  ),
                  Row(
                    children: [
                      GestureDetector(
                        onTap: () {
                          Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) =>
                                      const RegisterScreen()));
                        },
                        child: const Text("Create account",
                            textAlign: TextAlign.left,
                            style: TextStyle(
                                fontFamily: "BricolageGrotesque",
                                fontSize: 20,
                                color: Color.fromARGB(255, 43, 43, 43))),
                      ),
                    ],
                  )
                ],
              ),
            ),
            Form(
                key: _formKey,
                child: Container(
                  alignment: Alignment.center,
                  child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        const Padding(padding: EdgeInsets.all(5)),
                        FractionallySizedBox(
                            widthFactor: 0.9,
                            child: TextFormField(
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return "Please enter your email";
                                }
                                if (!_isEmailValid(value)) {
                                  return "Email is not valid";
                                }
                                return null;
                              },
                              controller: _emailController,
                              onChanged: (value) {
                                _areAllFieldsValid();
                              },
                              decoration: const InputDecoration(
                                suffixIcon: Icon(Icons.email),
                                labelText: "Email",
                                labelStyle: TextStyle(
                                    color: Color.fromARGB(255, 43, 43, 43)),
                              ),
                            )),
                        const Padding(padding: EdgeInsets.all(5)),
                        FractionallySizedBox(
                            widthFactor: 0.9,
                            child: TextFormField(
                              validator: (value) {
                                if (value == null || value.isEmpty) {
                                  return "Please enter your password";
                                }
                                if (value.length < 5) {
                                  return "Password must be at least 5 characters long";
                                }
                                return null;
                              },
                              obscureText: true,
                              controller: _passwordController,
                              onChanged: (value) {
                                _areAllFieldsValid();
                              },
                              decoration: const InputDecoration(
                                suffixIcon: Icon(Icons.lock),
                                border: UnderlineInputBorder(
                                    borderSide: BorderSide(
                                        color: Color.fromARGB(
                                            255, 212, 212, 212))),
                                labelText: "Password",
                                labelStyle: TextStyle(
                                  color: Color.fromARGB(255, 43, 43, 43),
                                ),
                              ),
                            )),
                        Row(
                          mainAxisAlignment: MainAxisAlignment.end,
                          children: [
                            Padding(
                                padding: EdgeInsets.only(right: 20, top: 10),
                                child: Consumer<LocaleModel>(
                                    builder: (context, localeModel, child) =>
                                        GestureDetector(
                                          onTap: () {
                                            localeModel.setLocale(Locale(
                                                localeModel.locale ==
                                                        Locale('en')
                                                    ? 'ro'
                                                    : 'en'));
                                          },
                                          child: Text(
                                            getAppLocalizations(context)!
                                                .forgotPassword,
                                            textAlign: TextAlign.right,
                                          ),
                                        )))
                          ],
                        ),
                        const Padding(padding: EdgeInsets.all(5)),
                        Container(
                            decoration: BoxDecoration(
                                shape: BoxShape.circle,
                                gradient: _isFormValid == true
                                    ? const LinearGradient(colors: [
                                        Color.fromARGB(255, 25, 77, 221),
                                        Color.fromARGB(255, 45, 3, 171),
                                      ])
                                    : const LinearGradient(colors: [
                                        Color.fromARGB(255, 128, 127, 127),
                                        Color.fromARGB(255, 128, 127, 127),
                                      ])),
                            child: ElevatedButton(
                                onPressed: _areAllFieldsValid() == true
                                    ? () {
                                        UserService.login(UserCredentials(
                                                email: _emailController.text,
                                                password:
                                                    _passwordController.text))
                                            .then((value) => {
                                                  if (value.result == true)
                                                    {
                                                      _storage.write(
                                                          key: "token",
                                                          value: value.token),
                                                    }
                                                  else
                                                    {
                                                      ScaffoldMessenger.of(
                                                              context)
                                                          .showSnackBar(SnackBar(
                                                              content: Text(
                                                                  value.error)))
                                                    }
                                                })
                                            .onError(
                                                (AuthResult error, stackTrace) {
                                          ScaffoldMessenger.of(context)
                                              .showSnackBar(SnackBar(
                                                  content: Text(
                                                      error.error.toString())));
                                          return Future(() => {});
                                        },
                                                test: (error) =>
                                                    error is AuthResult);
                                      }
                                    : null,
                                style: ElevatedButton.styleFrom(
                                  minimumSize: const Size(0, 52),
                                  elevation: 0,
                                  shape: const CircleBorder(eccentricity: 0),
                                  shadowColor: Colors.transparent,
                                  backgroundColor: Colors.transparent,
                                ),
                                child: _isFormValid == true
                                    ? const Icon(
                                        color: Colors.white,
                                        Icons.arrow_right_alt_rounded,
                                        size: 40,
                                      )
                                    : const Icon(
                                        color: Color.fromARGB(255, 73, 73, 73),
                                        Icons.arrow_right_alt_rounded,
                                        size: 40,
                                      ))),
                        const Padding(padding: EdgeInsets.all(10)),
                        const Column(
                          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                          children: [
                            LoginWithGoogleButton(),
                            Padding(padding: EdgeInsets.all(10)),
                            LoginWithFacebookButton()
                          ],
                        ),
                      ]),
                )),
          ]),
    );
  }
}
