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
        body: Stack(children: [
      const Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [LoginWithGoogleButton(), LoginWithFacebookButton()],
      ),
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
                            labelText: "Email",
                            labelStyle: TextStyle(
                                color: Color.fromARGB(255, 43, 43, 43)),
                          ),
                        )),
                    const Padding(padding: EdgeInsets.all(5)),
                    FractionallySizedBox(
                        widthFactor: 0.9,
                        child: TextFormField(
                          controller: passwordController,
                          decoration: const InputDecoration(
                            border: UnderlineInputBorder(
                                borderSide: BorderSide(
                                    color: Color.fromARGB(255, 212, 212, 212))),
                            labelText: "Password",
                            labelStyle: TextStyle(
                              color: Color.fromARGB(255, 43, 43, 43),
                            ),
                          ),
                        )),
                  ])))
    ]));
  }
}