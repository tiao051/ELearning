import 'package:flutter/material.dart';

class TopicListWidget extends StatelessWidget {
  final List<String> _topics;

  TopicListWidget({Key? key, required List<String> topics})
      : _topics = topics,
        super(key: key);

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 50, 
      child: ListView.builder(
        scrollDirection: Axis.horizontal,
        itemCount: _topics.length,
        itemBuilder: (BuildContext context, int index) {
          return _buildTopicButton(index);
        },
      ),
    );
  }

  Widget _buildTopicButton(int index) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 4.0),
      child: ElevatedButton(
        onPressed: () {
          print('Pressed topic: ${_topics[index]}');
        },
        child: Text(_topics[index]),
      ),
    );
  }
}
