import 'package:flutter/material.dart';
import 'package:frontend/utilities/custom_icons.dart';
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
    final _formKey = GlobalKey<FormState>();
    final _emailController = TextEditingController();
    final _passwordController = TextEditingController();

    return const Scaffold(
        body: Stack(children: [
      Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [LoginWithGoogleButton(), LoginWithFacebookButton()],
      ),
    ]));
  }
}
