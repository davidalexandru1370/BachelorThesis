import 'package:flutter/widgets.dart';
import 'package:frontend/utilities/custom_icons.dart';

import 'button.dart';

class LoginWithFacebookButton extends StatelessWidget {
  const LoginWithFacebookButton({super.key});

  @override
  Widget build(BuildContext context) => Button(
      text: "Google",
      onPressed: () {},
      icon: CustomIcons.facebook,
      iconSize: 36,
      iconColor: const Color.fromARGB(255, 65, 19, 231),
      textColor: const Color.fromARGB(255, 65, 19, 231),
      backgroundColor: const Color.fromARGB(255, 251, 249, 249),
      fontSize: 24);
}
