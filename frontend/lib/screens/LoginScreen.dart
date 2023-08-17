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
      Container(
        height: double.infinity,
        width: double.infinity,
        color: Color.fromARGB(255, 233, 233, 233),
        child: Row(children: [
          ElevatedButton.icon(
              onPressed: () {},
              icon: Icon(Icons.facebook),
              label: Text("Facebook"))
        ]),
      )
    ]));
  }
}
