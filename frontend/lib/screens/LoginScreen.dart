import 'package:flutter/material.dart';
import 'package:frontend/utilities/custom_icons.dart';

class LoginScreen extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  @override
  Widget build(BuildContext context) {
    final _formKey = GlobalKey<FormState>();
    final _emailController = TextEditingController();
    final _passwordController = TextEditingController();

    return Scaffold(
        body: Stack(children: [
      Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [
          OutlinedButton.icon(
              onPressed: () {},
              icon: const Icon(
                CustomIcons.facebook,
                size: 36.0,
                color: Color.fromARGB(255, 65, 19, 231),
              ),
              style: ButtonStyle(
                backgroundColor: MaterialStateProperty.all(
                    const Color.fromARGB(255, 251, 249, 249)),
                side: MaterialStateProperty.all(
                    const BorderSide(color: Colors.white, width: 0)),
                padding: MaterialStateProperty.all(
                    const EdgeInsets.symmetric(vertical: 10, horizontal: 15)),
              ),
              label: const Text("Facebook",
                  style: TextStyle(
                      fontFamily: "BricolageGrotesque",
                      fontSize: 25,
                      color: Color.fromARGB(255, 65, 19, 231)))),
          OutlinedButton.icon(
              onPressed: () {},
              icon: const Icon(
                Icons.facebook,
                size: 24.0,
              ),
              style: ButtonStyle(
                padding: MaterialStateProperty.all(
                    const EdgeInsets.symmetric(vertical: 10, horizontal: 15)),
              ),
              label: const Text("Facebook",
                  style: TextStyle(
                      fontFamily: "BricolageGrotesque", fontSize: 20))),
        ],
      ),
    ]));
  }
}
