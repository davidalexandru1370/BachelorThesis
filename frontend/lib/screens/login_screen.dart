import 'package:flutter/material.dart';
import 'package:frontend/widgets/login_with_facebook_button.dart';
import 'package:frontend/widgets/login_with_google_button.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<StatefulWidget> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  bool _isEmailValid(String email) =>
      RegExp(r"^[a-zA-Z0-9.]+@[a-zA-Z0-9]+\.[a-zA-Z]+").hasMatch(email);

  @override
  Widget build(BuildContext context) {
    final _formKey = GlobalKey<FormState>();
    final _emailController = TextEditingController();
    final _passwordController = TextEditingController();

    return Scaffold(
      resizeToAvoidBottomInset: false,
      body: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            const Padding(
              padding: EdgeInsets.only(left: 10),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  Row(
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
                      Text("Create account",
                          textAlign: TextAlign.left,
                          style: TextStyle(
                              fontFamily: "BricolageGrotesque",
                              fontSize: 20,
                              color: Color.fromARGB(255, 43, 43, 43))),
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
                        const Row(
                          mainAxisAlignment: MainAxisAlignment.end,
                          children: [
                            Padding(
                              padding: EdgeInsets.only(right: 20, top: 10),
                              child: Text(
                                "Forgot password",
                                textAlign: TextAlign.right,
                              ),
                            )
                          ],
                        ),
                        const Padding(padding: EdgeInsets.all(5)),
                        Container(
                            decoration: const BoxDecoration(
                                shape: BoxShape.circle,
                                gradient: LinearGradient(colors: [
                                  Color.fromARGB(255, 25, 77, 221),
                                  Color.fromARGB(255, 45, 3, 171),
                                ])),
                            child: ElevatedButton(
                                onPressed: () {
                                  if (_formKey.currentState!.validate()) {}
                                },
                                style: ElevatedButton.styleFrom(
                                  minimumSize: const Size(0, 52),
                                  elevation: 0,
                                  shape: const CircleBorder(eccentricity: 0),
                                  shadowColor: Colors.transparent,
                                  backgroundColor: Colors.transparent,
                                ),
                                child: const Icon(
                                  color: Colors.white,
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
