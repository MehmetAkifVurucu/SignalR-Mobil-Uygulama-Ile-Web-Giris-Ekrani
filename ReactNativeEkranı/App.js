import React, {Component} from 'react';
import {
  SafeAreaView,
  StyleSheet,
  ScrollView,
  View,
  Text,
  StatusBar,
  TouchableOpacity,
  FlatList,
  ActivityIndicator,
  TextInput,
  Dimensions,
} from 'react-native';

type Props = {};
export default class App extends Component<Props> {
  constructor(props) {
    super(props);
    this.state = {
      kullaniciId: "147852369",
      txtKod: "",
    };
  }

  dogrulama = () => {
    var param = {kullaniciId: this.state.kullaniciId, kod: this.state.txtKod};
    console.log(param);
    fetch('http://obs.akifvurucu.com/Home/DurumGuncelle', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(param),
    })
      .then(function(response) {
        return response.json();
      })
      .then(function(data) {
        ChromeSamples.log('Created Gist:', data.html_url);
      });
  };
  render() {
    return (
      <>
        <StatusBar barStyle="dark-content" />
        <View style={styles.container}>
          <Text style={styles.baslik}>Öğrenci İşleri</Text>
          <View style={styles.kutu1}>
            {/* <TextInput
            placeholder="Ad Soyad"
            style={styles.txtArkaplan}></TextInput>
          <TextInput placeholder="Yaş" style={styles.txtArkaplan}></TextInput>
          <TextInput placeholder="Telefon" style={styles.txtArkaplan}></TextInput> */}
            <TextInput
              placeholder="Doğrulama Kodu"
              style={styles.txtArkaplan}
              onChangeText={txtKod => this.setState({txtKod})}
            />
            <TouchableOpacity style={styles.btnKaydet}
            onPress={this.dogrulama}>
              <Text style={styles.btnYazi}>Onayla</Text>
            </TouchableOpacity>
          </View>
          <View style={styles.kutu2} />
        </View>
      </>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  kutu1: {
    flex: 2,
    backgroundColor: '#32a852',
    marginTop: 10,
  },
  kutu2: {
    flex: 1,
    backgroundColor: '#667a6b',
  },
  baslik: {
    textAlign: 'center',
  },
  txtArkaplan: {
    backgroundColor: '#fff',
    borderWidth: 1,
    borderColor: '#ddd',
    margin: 10,
    borderRadius: 15,
  },
  btnKaydet: {
    backgroundColor: '#7288c4',
    margin: 10,
    borderRadius: 15,
    height: 50,

    justifyContent: 'center',
    alignItems: 'center',
  },
  btnYazi: {
    color: '#fff',
  },
});
