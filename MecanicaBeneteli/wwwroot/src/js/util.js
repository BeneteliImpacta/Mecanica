class Util {

    rolarTelaParaCima() {
        window.parent.scrollTo(0, 0);
    }

    desabilitarElemento(idElemento) {
        document.getElementById(idElemento).disabled = true;
    }

    habilitarElemento(idElemento) {
        document.getElementById(idElemento).disabled = false;
    }
}
export default Util;