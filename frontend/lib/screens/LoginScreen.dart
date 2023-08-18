import 'package:flutter/material.dart';

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
      Padding(padding: EdgeInsets.all(20)),
      Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [
          OutlinedButton.icon(
              onPressed: () {},
              icon: const Icon(
                Icons.facebook,
                size: 36.0,
                color: Colors.blue,
              ),
              style: ButtonStyle(
                backgroundColor: MaterialStateProperty.all(
                    Color.fromARGB(255, 251, 249, 249)),
                side: MaterialStateProperty.all(
                    const BorderSide(color: Colors.white, width: 0)),
                padding: MaterialStateProperty.all(
                    const EdgeInsets.symmetric(vertical: 10, horizontal: 15)),
              ),
              label: const Text("Facebook",
                  style: TextStyle(fontFamily: "Arial", fontSize: 20))),
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
                  style: TextStyle(fontFamily: "Arial", fontSize: 20))),
        ],
      ),
    ]));
  }
}
