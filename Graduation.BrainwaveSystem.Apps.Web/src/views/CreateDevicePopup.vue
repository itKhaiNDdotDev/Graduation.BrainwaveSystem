<template>
    <div class="popup__wrapper">
        <button>TEST</button>
        <Input></Input>
    </div>
</template>

<script>
export default {
    name: "CreateDevicePopup"
}
</script>

<style>
.popup__wrapper {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 900px;
    height: 600px;
    background-color: yellow;
}
</style>