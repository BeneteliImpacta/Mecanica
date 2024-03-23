
import { Tooltip, Toast, Popover } from 'bootstrap';
import LayoutThemeApp from './layout/index';
import 'simplebar';

import '../scss/app.scss';
import '../scss/icons.scss';
import '../css/nsca.css';

import Portlet from "./components/portlet";
import ValidationForm from './ui/forms/validation';

class App {

    constructor() {
        this.body = document.body;
        this.window = window;
        this.layoutThemeApp = new LayoutThemeApp();

        this.entityMap = {
            "&": "&amp;",
            "<": "&lt;",
            ">": "&gt;",
            '"': '&quot;',
            "'": '&#39;',
            "/": '&#x2F;'
        };
    }

    fadeOutEffect(fadeTarget, slow = true) {
        if (fadeTarget === null)
            return;
        fadeTarget.style.opacity = 1;

        const opacitySpeed = slow ? 0.05 : 0.2;
        const timeSpeed = slow ? 80 : 100;

        var fadeEffect = setInterval(function () {
            if (fadeTarget.style.opacity < opacitySpeed) {
                fadeTarget.style.display = "none";
                clearInterval(fadeEffect);
            }
            else {
                fadeTarget.style.opacity -= opacitySpeed;
            }
        }, timeSpeed);
    }

    init = () => {
        this.layoutThemeApp.init();
        this.initToast();
        this.initPopover();
        this.initTooltip();
        new Portlet().init();
        this.loading();
        this.initShowHidePassword();
        this.selecionarIconeBotaoMenu();

        window.onresize = this.selecionarIconeBotaoMenu;
    }

    loading() {
        const self = this;
        // remove loading
        setTimeout(function () {
            self.body.classList.remove('loading');
        }, 400);
    }

    initTooltip() {

        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new Tooltip(tooltipTriggerEl)
        })

    }

    initPopover() {
        var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
        var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
            return new Popover(popoverTriggerEl)
        })
    }

    initToast() {
        var toastElList = [].slice.call(document.querySelectorAll('.toast'))
        var toastList = toastElList.map(function (toastEl) {
            return new Toast(toastEl)
        })
    }


    initShowHidePassword = function () {
        document.querySelectorAll("[data-password]").forEach(function (element) {
            element.addEventListener('click', function (event) {
                if (element.getAttribute("data-password") == "false") {
                    element.parentElement.querySelector('input').setAttribute("type", "text");
                    element.setAttribute("data-password", "true");
                    element.classList.add("show-password");
                } else {
                    element.parentElement.querySelector('input').setAttribute("type", "password");
                    element.setAttribute("data-password", "false");
                    element.classList.remove("show-password");
                }
            });
        });
    }

    selecionarIconeBotaoMenu = function() {
        let botao = document.getElementById('botao-menu-icone');
        const classesTelaGrande = ["mdi", "mdi-arrow-collapse-horizontal"];
        const classesTelaPequena = ["mdi", "mdi-menu"];

        if (botao != null) {
            if (window.screen.width >= 768) {
                botao.classList.remove(...classesTelaPequena);
                botao.classList.add(...classesTelaGrande);
            }
            else {
                botao.classList.remove(...classesTelaGrande);
                botao.classList.add(...classesTelaPequena);
            }
        }
    }
}

window.addEventListener('load', function () {
    window.app = new App();
    window.app.init();
})
