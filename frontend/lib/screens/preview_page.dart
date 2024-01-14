import 'dart:io';

import 'package:camera/camera.dart';
import 'package:flutter/material.dart';

class PreviewPage extends StatelessWidget {
  final XFile picture;

  const PreviewPage({Key? key, required this.picture}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;

    return Scaffold(
      body: Center(
          child: SizedBox(
              width: size.width,
              height: size.height,
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Image.file(File(picture.path),
                      fit: BoxFit.contain,
                      width: 0.9 * size.width,
                      height: 0.8 * size.height),
                ],
              ))),
    );
  }
}
