import 'package:flutter/material.dart';
import 'package:frontend/widgets/login_with_facebook_button.dart';
import 'package:frontend/widgets/login_with_google_button.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<StatefulWidget> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  @override
  Widget build(BuildContext context) {
    final formKey = GlobalKey<FormState>();
    final emailController = TextEditingController();
    final passwordController = TextEditingController();

    return Scaffold(
        appBar: AppBar(
          toolbarHeight: 10,
        ),
        body: Stack(alignment: Alignment.bottomLeft, children: [
          Form(
              key: formKey,
              child: Container(
                alignment: Alignment.center,
                child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      FractionallySizedBox(
                          widthFactor: 0.9,
                          child: TextFormField(
                            controller: emailController,
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
                            obscureText: true,
                            controller: passwordController,
                            decoration: const InputDecoration(
                              suffixIcon: Icon(Icons.lock),
                              border: UnderlineInputBorder(
                                  borderSide: BorderSide(
                                      color:
                                          Color.fromARGB(255, 212, 212, 212))),
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
                              onPressed: () {},
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
        ]));
  }
}
