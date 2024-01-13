import 'package:camera/camera.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class CameraScreen extends StatefulWidget {
  const CameraScreen({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() {
    return _CameraScreenState();
  }
}

class _CameraScreenState extends State<CameraScreen> {
  late CameraController _cameraController;
  bool isCameraReady = false;

  @override
  void initState() {
    super.initState();
    _initializeCamera();
  }

  @override
  Widget build(BuildContext context) {
    return isCameraReady == true &&
            _cameraController.value.isInitialized == true
        ?  CameraPreview(_cameraController)
        : const Center(child: CircularProgressIndicator());
  }

  @override
  void dispose() {
    _cameraController.dispose();
    super.dispose();
  }

  Future<void> _initializeCamera() async {
    final cameras = await availableCameras();
    final rearCamera = cameras.firstWhere(
        (camera) => camera.lensDirection == CameraLensDirection.back);

    _cameraController = CameraController(rearCamera, ResolutionPreset.max);
    try {
      _cameraController.initialize().then((_) {
        if (!mounted) {
          return;
        }
        setState(() {
          isCameraReady = true;
        });
      });
    } on CameraException catch (e) {
      debugPrint("Camera Error: $e");
    }
  }
}
