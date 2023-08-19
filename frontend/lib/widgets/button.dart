import 'package:flutter/material.dart';

class Button extends StatelessWidget {
  final String text;
  final Function onPressed;
  final IconData icon;
  final double iconSize;
  final Color iconColor;
  final Color textColor;
  final Color backgroundColor;
  final double fontSize;

  const Button({
    Key? key,
    required this.text,
    required this.onPressed,
    required this.icon,
    required this.iconSize,
    required this.iconColor,
    required this.textColor,
    required this.backgroundColor,
    required this.fontSize,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return OutlinedButton.icon(
        onPressed: () {
          onPressed();
        },
        icon: Icon(
          icon,
          size: iconSize,
          color: iconColor,
        ),
        style: ButtonStyle(
          backgroundColor: MaterialStateProperty.all(backgroundColor),
          side: MaterialStateProperty.all(
              const BorderSide(color: Colors.white, width: 0)),
          padding: MaterialStateProperty.all(
              const EdgeInsets.symmetric(vertical: 10, horizontal: 15)),
        ),
        label: Text(text,
            style: TextStyle(
                fontFamily: "BricolageGrotesque",
                fontSize: fontSize,
                color: textColor)));
  }
}
