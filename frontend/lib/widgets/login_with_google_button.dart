import 'package:flutter/material.dart';
import 'package:frontend/utilities/custom_icons.dart';
import 'package:frontend/widgets/button.dart';

class LoginWithGoogleButton extends StatelessWidget {
  const LoginWithGoogleButton({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) => Button(
      text: "Google",
      onPressed: () {},
      icon: CustomIcons.google,
      iconSize: 36,
      iconColor: const Color.fromARGB(255, 65, 19, 231),
      textColor: const Color.fromARGB(255, 65, 19, 231),
      backgroundColor: const Color.fromARGB(255, 251, 249, 249),
      fontSize: 24);
}
